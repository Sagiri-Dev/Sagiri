using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Reactive.Disposables;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Toolkit.Uwp.Notifications;

using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

using Sagiri.Services.Misskey;
using Sagiri.Services.Misskey.Interfaces;
using Sagiri.Services.Spotify;
using Sagiri.Services.Spotify.Interfaces;
using Sagiri.Services.Spotify.Track;
using Sagiri.Util.Common;
using SagiriApp.Controls;
using SagiriApp.Interop;

using IDisposable = System.IDisposable;

namespace SagiriApp.Models
{
    internal class SagiriModel : BindableBase, IDisposable
    {
        #region Properties

        private ISpotifyService _ISpotifyService { get; init; } = new SpotifyService();
        private IMisskeyService _IMisskeyService { get; init; } = new MisskeyService();

        private Logger _Logger { get; set; } = Logger.GetInstance;

        private static Lazy<HttpClient> _Client { get; set; } = new();
        private CancellationTokenSource _CancellationSource { get; init; } = new();

        private SettingJsonModel _Setting { get; set; } = new();

        public ReactivePropertySlim<bool> IsSpotifyPlaying { get; set; }
        public ReactivePropertySlim<CurrentTrackInfo> CurrentTrackInfo { get; set; }
        public ReactivePropertySlim<string> PostingFormat { get; set; }

        private readonly CompositeDisposable _cd = new();

        private string _TrackTitleOld { get; set; } = default!;
        private string _ArtistOld { get; set; } = default!;
        private int _ByteStreamLengthOld { get; set; } = default!;

        private bool _IsSetupCompleted { get; set; } = false;

        #endregion Properties

        #region Constructor

        internal SagiriModel() { }

        #endregion Constructor

        #region Public Methods

        public void Dispose()
        {
            _ISpotifyService?.Dispose();
            _IMisskeyService?.Dispose();
            
            CurrentTrackInfo.AddTo(_cd);
            IsSpotifyPlaying.AddTo(_cd);
            PostingFormat.AddTo(_cd);
            _cd.Dispose();

            _CancellationSource?.Dispose();
            _Logger = null; 
        }

        #endregion Public Methods

        #region Internal Methods

        internal async ValueTask InitializeAsync()
        {
            if (!File.Exists("spotify.json") || !File.Exists("misskey.json"))
                _NotifyErrorCredentialInfo(errorType: "file", title: "File not found!", message: "");

            var isSpotifyInitializeCompleted = await _ISpotifyService.InitializeAsync();
            if (!isSpotifyInitializeCompleted)
            {
                var msg = "Spotify token is an invalid value or empty...";
                _NotifyErrorCredentialInfo(errorType: "token", title: "Token info an invalid!", message: msg);
            }

            var isMisskeyInitializeCompleted = await _IMisskeyService.InitializeAsync();
            if (!isMisskeyInitializeCompleted)
            {
                var msg = "Misskey token or Host(URL) is an invalid value or empty...";
                _NotifyErrorCredentialInfo(errorType: "token", title: "Token info an invalid!", message: msg);
            }

            _ISpotifyService.CurrentTrackChanged += _OnSpotifyCurrentlyPlayingChanged;
            _ISpotifyService.CurrentTrackErrorDetected += _OnSpotifyCurrentTrackErrorDetected;

            await _ISpotifyService.StartAsync(_CancellationSource.Token).ConfigureAwait(false);

            // TODO - spotify account icon relation-ship.
            //var accountImageStream = await _ISpotifyService.GetUserImageStream();
            //AccountIconImage.Value ??= Helper.ConvertBitmapToBitmapImage(new Bitmap(accountImageStream));

            _Setting = await _Setting.LoadAsync();
            PostingFormat = new ReactivePropertySlim<string>(_Setting.PostingFormat);
            RaisePropertyChanged(nameof(PostingFormat));

            _Logger.WriteLog("[SagiriApp] - Finished roading window....", Logger.LogLevel.Info);
        }

        internal async ValueTask StartAsync()
        {
            _ISpotifyService.CurrentTrackChanged += _OnSpotifyCurrentlyPlayingChanged;
            await _ISpotifyService.StartAsync(_CancellationSource.Token).ConfigureAwait(false);

            _IsSetupCompleted = true;

            _Logger.WriteLog("[SagiriApp] - Finished roading SagiriWindow....", Logger.LogLevel.Debug);
        }

        internal async ValueTask PostMisskeyAsync()
        {
            // Don't run until everything is ready.
            if (!_IsSetupCompleted)
            {
                new MessageBoxEx("Spotify 側認証が完了していないため、Misskeyへの投稿はできません。", "投稿失敗:-(", 2000).Show();
                return;
            }

            var client = _Client.Value;
            var postText = Helper.GenerateTrackText(_Setting.PostingFormat, CurrentTrackInfo.Value);

            var byteStream = await client.GetByteArrayAsync(CurrentTrackInfo.Value.ArtworkUrl);
            var ps = new Dictionary<string, object> {
                { "text", postText },
                { "visibility", "home" }
            };

            var form = new MultipartFormDataContent {
                { new ByteArrayContent(byteStream), "file", "cover.jpg" }
            };

            var (file, isUploadCompleted) = await _IMisskeyService.RequestWithBinaryAsync("drive/files/create", form);
            if (!isUploadCompleted)
            {
                new MessageBoxEx("Misskeyへの画像アップロードに失敗しました。\r\n画像なしの投稿に切り替えます。", "アップロード失敗:-(", 2000).Show();
                _Logger.WriteLog($"[SagiriApp] - Misskeyへの画像アップロードに失敗 - 画像なし投稿モード", Logger.LogLevel.Error);
            }
            else
                ps.Add("mediaIds", new string[] { file.id });

            var (_, isNotePosted) = await _IMisskeyService.RequestAsync("notes/create", ps);
            if (!isNotePosted)
            {
                new MessageBoxEx("Misskeyへの投稿に失敗しました。\r\n少し時間を置いて再度投稿してください。", "投稿失敗:-(", 2000).Show();
                _Logger.WriteLog($"[SagiriApp] - Misskeyへの投稿に失敗", Logger.LogLevel.Error);
                return;
            }

            new MessageBoxEx(postText, "投稿完了:-)", 2000).Show();

            #region Logging

            _Logger.WriteLog(
                $"Posted on Misskey -> " +
                $"🎵 {CurrentTrackInfo.Value.TrackTitle} - " +
                $"🎙 {CurrentTrackInfo.Value.Artist} - " +
                $"💿 {CurrentTrackInfo.Value.Album}",
                Logger.LogLevel.Info
            );

            #endregion Logging
        }

        internal static void ViewAlbumArt()
        {
            if (!File.Exists("cover.jpg"))
                return;

            var p = new Process() {
                StartInfo = new ProcessStartInfo(@"cover.jpg") { UseShellExecute = true }
            };
            p.Start();
        }

        internal void SaveSetting()
        {
            _Setting.PostingFormat = PostingFormat.Value;
            _Setting.SaveAsync();
            new MessageBoxEx("投稿フォーマットを保存しました。", "投稿フォーマット保存完了！", 2000).Show();

            _Logger.WriteLog($"[SagiriApp] - 投稿フォーマット保存完了！", Logger.LogLevel.Info);
        }

        #endregion Internal Methods

        #region Private Methods

        private void _OnSpotifyCurrentlyPlayingChanged(CurrentTrackInfo trackInfo)
        {
            if (trackInfo is null)
            {
                _Logger.WriteLog("[SagiriApp] - CurrentTrackInfo is null...", Logger.LogLevel.Debug);
                return;
            }

            if (trackInfo.TrackTitle == _TrackTitleOld && trackInfo.Artist == _ArtistOld)
                return;

            _TrackTitleOld = trackInfo.TrackTitle;
            _ArtistOld = trackInfo.Artist;

            IsSpotifyPlaying = new ReactivePropertySlim<bool>(trackInfo.IsPlaying);
            RaisePropertyChanged(nameof(IsSpotifyPlaying));

            CurrentTrackInfo = new ReactivePropertySlim<CurrentTrackInfo>(trackInfo);
            RaisePropertyChanged(nameof(CurrentTrackInfo));

            #region Saving Current AlbumArt

            _ = Task.Factory.StartNew(async () =>
            {
                var client = _Client.Value;
                var byteStream = await client.GetByteArrayAsync(trackInfo.ArtworkUrl);

                if (byteStream.Length == _ByteStreamLengthOld)
                    return;
                _ByteStreamLengthOld = byteStream.Length;

                var saveName = "cover.jpg";
                using MemoryStream ms = new(byteStream);

                Bitmap bmp = new(ms);
                bmp.Save(saveName);
            });

            this._NotifyToastCurrentTrackInfo(trackInfo);

            #endregion #region Saving Current AlbumArt

            #region Logging

            _Logger.WriteLog("[SagiriApp] - Called OnSpotifyCurrentlyPlayingChanged", Logger.LogLevel.Debug);
            _Logger.WriteLog(
                $"[SagiriApp] - OnSpotifyCurrentlyPlayingChanged -> " +
                $"🎵 {trackInfo.TrackTitle} - " +
                $"🎙 {trackInfo.Artist} - " +
                $"💿 {trackInfo.Album}",
                Logger.LogLevel.Info
            );

            #endregion Logging
        }

        private void _OnSpotifyCurrentTrackErrorDetected()
        {
            MessageBox.Show("Spotify CurrentTrackError...", "Sagiri-NowPlaying Closed...");
            _Logger.WriteLog("Spotify CurrentTrackError...", Logger.LogLevel.Error);

            _ISpotifyService?.Dispose();
        }

        private void _NotifyErrorCredentialInfo(string errorType, string title, string message = "")
        {
            if (errorType is "file")
            {
                if (!File.Exists("spotify.json"))
                    MessageBox.Show("spotify.json file not found...", title);

                if (!File.Exists("misskey.json"))
                    MessageBox.Show("misskey.json file not found...", title);

                _Logger.WriteLog("token file not found...", Logger.LogLevel.Fatal);
            }
            else
            {
                MessageBox.Show(message, title);
                _Logger.WriteLog(message, Logger.LogLevel.Fatal);
            }

            MessageBox.Show("アプリを終了し、各種認証情報を見直してください。", "認証情報エラー");
            return;
        }

        private void _NotifyToastCurrentTrackInfo(CurrentTrackInfo trackInfo)
        {
            var notifyText = Helper.GenerateTrackText(_Setting.PostingFormat, trackInfo);

            // Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
            new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
                .AddText("Sagiri-NowPlaying🎵")
                .AddText(notifyText)
                .Show();
        }

        #endregion Private Methods
    }
}
