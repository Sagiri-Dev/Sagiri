using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Sagiri.Services.Misskey;
using Sagiri.Services.Misskey.Interfaces;
using Sagiri.Services.Spotify;
using Sagiri.Services.Spotify.Interfaces;
using Sagiri.Services.Spotify.Track;
using Sagiri.Util.Common;
using SagiriUI.Controls;
using SagiriUI.Properties;
using SagiriUI.Settings;

using Microsoft.Toolkit.Uwp.Notifications;

namespace SagiriUI
{
    public partial class Form1 : Form
    {
        #region Properties

        private ISpotifyService _ISpotifyService { get; set; }
        private IMisskeyService? _IMisskeyService { get; set; }
        private CurrentTrackInfo _CurrentTrackInfo { get; set; }

        private SettingJsonFile _Setting { get; set; }
        private Logger _Logger { get; set; }
        private Point _MousePoint { get; set; }

        private static Lazy<HttpClient> _Client { get; set; } = new();
        private CancellationTokenSource _CancellationSource { get; set; } = new();

        private string _TrackTitleOld { get; set; } = default!;
        private string _ArtistOld { get; set; } = default!;
        private int _ByteStreamLengthOld { get; set; } = default!;

        #endregion Properties

        #region Constructor

        public Form1() => InitializeComponent();

        #endregion Constructor

        #region Private Methods

        private async void Form1_Load(object sender, EventArgs e)
        {
            _Logger = Logger.GetInstance;

            if (!File.Exists("spotify.json") || !File.Exists("misskey.json"))
                _CheckClosingApp(errorType: "file", title: "File not found!", message: "");

            _ISpotifyService = new SpotifyService();
            _CurrentTrackInfo = new();

            var spCanInitialized = await _ISpotifyService.InitializeAsync();
            if (!spCanInitialized)
            {
                var msg = "[SagiriUI] - Spotify token is an invalid value or empty...";
                _CheckClosingApp(errorType: "token", title: "Token info an invalid!", message: msg);
            }

            _IMisskeyService = new MisskeyService();
            var miCanInitalized = await _IMisskeyService.InitializeAsync();
            if (!miCanInitalized)
            {
                var msg = "Misskey token or Host(URL) is an invalid value or empty...";
                _CheckClosingApp(errorType: "token", title: "Token info an invalid!", message: msg);
            }

            _ISpotifyService.CurrentTrackChanged += _OnSpotifyCurrentlyPlayingChanged;
            _ISpotifyService.CurrentTrackErrorDetected += _OnSpotifyCurrentTrackErrorDetected;

            await _ISpotifyService.StartAsync(_CancellationSource.Token).ConfigureAwait(false);

            var accountImageStream = await _ISpotifyService.GetUserImageStream();
            AccountPanel.BackgroundImage = Image.FromStream(accountImageStream) ?? Resources.account;

            _Setting = await SettingJsonFile.LoadAsync();

            _Logger.WriteLog("[SagiriUI] - Finished roading Form....", Logger.LogLevel.Info);
        }

        private void Form1_Closing(object sender, EventArgs e)
        {
            _ISpotifyService?.Dispose();
            _IMisskeyService?.Dispose();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                _MousePoint = new Point(e.X, e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Left += e.X - _MousePoint.X;
                this.Top += e.Y - _MousePoint.Y;
            }
        }

        private void _CheckClosingApp(string errorType, string title, string message = "")
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

            MessageBox.Show("Close this app.", "Close App!");
            this.Close();
            return;
        }

        private void _OnSpotifyCurrentTrackErrorDetected()
        {
            MessageBox.Show("Spotify CurrentTrackError...", "Sagiri Closed...");
            _Logger.WriteLog("Spotify CurrentTrackError...", Logger.LogLevel.Error);

            _ISpotifyService.Dispose();
            this.Invoke((MethodInvoker)(() => this.Close()));
        }

        private void _OnSpotifyCurrentlyPlayingChanged(CurrentTrackInfo trackInfo)
        {
            if (trackInfo is null)
            {
                _Logger.WriteLog("CurrentTrackInfo is null...", Logger.LogLevel.Debug);
                return;
            }

            _CurrentTrackInfo = trackInfo;
            this.Invoke((MethodInvoker)(() => pictureBoxAlbumArt.ImageLocation = trackInfo.ArtworkUrl));

            if (trackInfo.TrackTitle == _TrackTitleOld && trackInfo.Artist == _ArtistOld)
                return;

            #region Saving AlbumArt

            // Saving Current AlbumArt
            _ = Task.Factory.StartNew(async () =>
            {
                if (trackInfo.TrackTitle == _TrackTitleOld && trackInfo.Artist == _ArtistOld)
                    return;
                _TrackTitleOld = trackInfo.TrackTitle;
                _ArtistOld = trackInfo.Artist;

                using HttpClient httpClient = new(new HttpClientHandler());
                var byteStream = await httpClient.GetByteArrayAsync(trackInfo.ArtworkUrl);
                if (byteStream.Length == _ByteStreamLengthOld)
                    return;
                _ByteStreamLengthOld = byteStream.Length;

                var saveName = "cover.jpg";
                using MemoryStream ms = new(byteStream);
                Bitmap bmp = new(ms);
                bmp.Save(saveName);
            });

            this._NotifyToastCurrentTrackInfo(trackInfo);

            #endregion Saving AlbumArt

            #region Logging

            _Logger.WriteLog("Called OnSpotifyCurrentlyPlayingChanged", Logger.LogLevel.Debug);
            _Logger.WriteLog(
                $"OnSpotifyCurrentlyPlayingChanged -> " +
                $"🎵 {_CurrentTrackInfo.TrackTitle} - " +
                $"🎙 {_CurrentTrackInfo.Artist} - " +
                $"💿 {_CurrentTrackInfo.Album}",
                Logger.LogLevel.Info
            );

            #endregion Logging
        }

        private void _NotifyToastCurrentTrackInfo(CurrentTrackInfo trackInfo)
        {
            var viewText = Helper.GenerateTrackText(_Setting.PostingFormat, trackInfo);

            // Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
            new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
                .AddText("Sagiri-NowPlaying🎵")
                .AddText(viewText)
                //.AddInlineImage(new Uri(trackInfo.ArtworkUrl))
                .Show();
        }

        private async void MisskeyPostPanel_Click(object sender, EventArgs e)
        {
            var client = _Client.Value;
            var postText = Helper.GenerateTrackText(_Setting.PostingFormat, _CurrentTrackInfo);

            var byteStream = await client.GetByteArrayAsync(_CurrentTrackInfo.ArtworkUrl);
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
                _Logger.WriteLog($"Misskeyへの画像アップロードに失敗 - 画像なし投稿モード", Logger.LogLevel.Error);
            }
            else
                ps.Add("mediaIds", new string[] { file.id });

            var (_, isNotePosted) = await _IMisskeyService.RequestAsync("notes/create", ps);
            if (!isNotePosted)
            {
                new MessageBoxEx("Misskeyへの投稿に失敗しました。\r\n少し時間を置いて再度投稿してください。", "投稿失敗:-(", 2000).Show();
                _Logger.WriteLog($"Misskeyへの投稿に失敗", Logger.LogLevel.Error);
                return;
            }

            new MessageBoxEx(postText, "投稿完了:-)", 2000).Show();

            #region Logging

            _Logger.WriteLog(
                $"Posted on Misskey -> " +
                $"🎵 {_CurrentTrackInfo.TrackTitle} - " +
                $"🎙 {_CurrentTrackInfo.Artist} - " +
                $"💿 {_CurrentTrackInfo.Album}",
                Logger.LogLevel.Info
            );

            #endregion Logging
        }

        private async void AccountPanel_Click(object sender, EventArgs e)
        {
            Process process = new();
            var url = await _ISpotifyService.GetUserProfileUrl();
            try
            {
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = url;
                process.Start();
            }
            catch (Exception)
            {
                var message = $"Unable to open a browser. Please manually open: {url}";
                _Logger.WriteLog(message, Logger.LogLevel.Info);
                throw new Exception(message);
            }
        }

        private void ClosePanel_Click(object sender, EventArgs e)
        {
            if (!this.IsDisposed && !this.Disposing)
                this.Close();
        }

        private void pictureBoxAlbumArt_Click(object sender, EventArgs e)
        {
            if (!File.Exists("cover.jpg"))
                return;

            var p = new Process(){
                StartInfo = new ProcessStartInfo(@"cover.jpg") { UseShellExecute = true }
            };
            p.Start();
        }

        private void InfoPanel_Click(object sender, EventArgs e) =>
            new InfoWindow(_CurrentTrackInfo).Show();

        private async void settingPanel_Click(object sender, EventArgs e)
        {
            var settingWindow = new SettingWindow(_Setting);

            if (settingWindow.ShowDialog() == DialogResult.OK)
                await this._Setting.SaveAsync();
        }

        private void TitlePanel_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                _MousePoint = new Point(e.X, e.Y);
        }

        private void TitlePanel_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Left += e.X - _MousePoint.X;
                this.Top += e.Y - _MousePoint.Y;
            }
        }

        #endregion Private Methods
    }
}
