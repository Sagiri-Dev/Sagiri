using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Sagiri.Services.Misskey;
using Sagiri.Services.Misskey.Interfaces;
using Sagiri.Services.Spotify;
using Sagiri.Services.Spotify.Interfaces;
using Sagiri.Services.Spotify.Track;
using Sagiri.Util.Common;
using Sagiri.Util.Configuration;
using SagiriUI.Properties;
using SagiriSelenium.Interfaces;

using Twist;
using SagiriUI.Controls;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Collections.Generic;

namespace SagiriUI
{
    public partial class Form1 : Form
    {
        #region Properties

        private ISpotifyService _ISpotifyService { get; set; }
        private IMisskeyService? _IMisskeyService { get; set; }
        private ISagiriSelenium _ISagiriSelenium { get; set; }
        private CurrentTrackInfo _CurrentTrackInfo { get; set; }

        private SpotifyCredentialConfig _SpotifyCredentialConfig { get; set; }
        private TwitterCredentialConfig _TwitterCredentialConfig { get; set; }

        private Twitter _Twist { get; set; }
        private Logger _Logger { get; set; }
        private Point _MousePoint { get; set; }

        private CancellationTokenSource _CancellationSource { get; set; } = new();

        private string _TrackTitleOld { get; set; } = default!;
        private string _ArtistOld { get; set; } = default!;
        private int _ByteStreamLengthOld { get; set; } = default!;

        private static Lazy<HttpClient> _Client { get; set; } = new();

        #endregion Properties

        #region Constructor

        public Form1() => InitializeComponent();

        #endregion Constructor

        #region Private Methods

        private async void Form1_Load(object sender, EventArgs e)
        {
            _IMisskeyService = new MisskeyService();
            await _IMisskeyService.InitializeAsync();

            _ISpotifyService = new SpotifyService();
            _CurrentTrackInfo = new();

            _SpotifyCredentialConfig = SpotifyCredentialConfig.Instance;
            _TwitterCredentialConfig = TwitterCredentialConfig.Instance;

            _Logger = Logger.GetInstance;

            if (_TwitterCredentialConfig.IsExistCredentialFile())
            {
                var twConfig = await _TwitterCredentialConfig.LoadCredentialAsync();
                _Twist = new Twitter(
                    twConfig.ConsumerKey,
                    twConfig.ConsumerSecret,
                    twConfig.AccessToken,
                    twConfig.AccessTokenSecret,
                    new HttpClient(new HttpClientHandler())
                );
                _Logger.WriteLog("[SagiriUI] - Finish read twitter credential info.", Logger.LogLevel.Debug);
            }
            else
            {
                var message = "[SagiriUI] - Twitter tokens file not found...\r\nCan't sent now-playing twitter.";
                MessageBox.Show(message);
                _Logger.WriteLog(message, Logger.LogLevel.Error);
            }

            await _ISpotifyService.InitializeAsync();
            if (_SpotifyCredentialConfig.IsExistCredentialFile())
            {
                _ISpotifyService.CurrentTrackChanged += _OnSpotifyCurrentlyPlayingChanged;
                _ISpotifyService.CurrentTrackErrorDetected += _OnSpotifyCurrentTrackErrorDetected;

                await _ISpotifyService.StartAsync(_CancellationSource.Token).ConfigureAwait(false);

                var accountImageStream = await _ISpotifyService.GetUserImageStream();
                AccountPanel.BackgroundImage = System.Drawing.Image.FromStream(accountImageStream) ?? Resources.account;
            }
            else
            {
                var message = "[SagiriUI] - Spotify tokens file not found...\r\nClose this app.";
                MessageBox.Show(message);
                _Logger.WriteLog(message, Logger.LogLevel.Fatal);

                this.Close();
            }

            _Logger.WriteLog("[SagiriUI] - Finished roading Form....", Logger.LogLevel.Info);
        }

        private void Form1_Closing(object sender, EventArgs e)
        {
            _ISpotifyService.Dispose();
            _ISagiriSelenium.Dispose();
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

        private void _OnSpotifyCurrentTrackErrorDetected()
        {
            MessageBox.Show("Spotify CurrentTrackError...", "Sagiri Closed...");
            _Logger.WriteLog("Spotify CurrentTrackError...", Logger.LogLevel.Error);

            _ISpotifyService.Dispose();
            _ISagiriSelenium.Dispose();

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

            #region Saving AlbumArt

            // Selenium 経由での画像投稿用に AlbumArt 保存
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

            if (trackInfo.TrackTitle != _TrackTitleOld && trackInfo.Artist != _ArtistOld)
            {
                _Logger.WriteLog("Called OnSpotifyCurrentlyPlayingChanged", Logger.LogLevel.Debug);
                _Logger.WriteLog(
                    $"OnSpotifyCurrentlyPlayingChanged -> " +
                    $"🎵 {_CurrentTrackInfo.TrackTitle} - " +
                    $"🎙 {_CurrentTrackInfo.Artist} - " +
                    $"💿 {_CurrentTrackInfo.Album}",
                    Logger.LogLevel.Info
                );
            }

            #endregion Logging
        }

        private void _NotifyToastCurrentTrackInfo(CurrentTrackInfo trackInfo)
        {
            if (trackInfo.TrackTitle == _TrackTitleOld && trackInfo.Artist == _ArtistOld)
                return;

            StringBuilder sb = new();
            sb.Append($"title: {_CurrentTrackInfo.TrackTitle}\r\n");
            sb.Append($"Artist: {_CurrentTrackInfo.Artist}\r\n");
            sb.Append($"Album: {_CurrentTrackInfo.Album}\r\n");

            // Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
            new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
                .AddText("Sagiri-NowPlaying🎵")
                .AddText(sb.ToString())
                //.AddInlineImage(new Uri(trackInfo.ArtworkUrl))
                .Show();
        }

        private async void NowPlayingPanel_Click(object sender, EventArgs e)
        {
            #region ★Dropped★ - Twitter API v2.0

#if false
            // ★Twitter API v2.0の場合だと、1500post / month しかできないため、Selenium スクレイピング投稿に変更
            //var tw = new StringBuilder();
            //tw.Append($"🎵 {_CurrentTrackInfo.TrackTitle}\r\n");
            //tw.Append($"🎙 {_CurrentTrackInfo.Artist}\r\n");
            //tw.Append($"💿 {_CurrentTrackInfo.Album}\r\n");
            //tw.Append("#nowplaying #Sagiri");

            //using var httpClient = new HttpClient(new HttpClientHandler());
            //var byteStream = await httpClient.GetByteArrayAsync(_CurrentTrackInfo.ArtworkUrl);
            //using var artworkStream = new MemoryStream(byteStream);
            //await _Twist.UpdateWithMediaAsync(tw.ToString(), artworkStream);
#endif

            #endregion Twitter API v2.0

            #region Selenium Side

            var tw = new StringBuilder();
            tw.Append($"{_CurrentTrackInfo.TrackTitle} - {_CurrentTrackInfo.Artist} ");
            tw.Append("#nowplaying #Sagiri");

            _ISagiriSelenium = new SagiriSelenium.SagiriSelenium();

            // $@"https://twitter.com/intent/tweet?text={tw.ToString()}" is Dropped
            var canPost = await _ISagiriSelenium.RunSeleniumAndPrePostTwitterAsync(@"https://twitter.com/", tw.ToString());

            if (!canPost)
            {
                MessageBox.Show("Post 前準備に失敗しました。", "投稿失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _Logger.WriteLog("[SagiriUI] - Post前準備に失敗しました。", Logger.LogLevel.Error);
                return;
            }

            await Task.Delay(500);
            SendKeys.SendWait($@"{Environment.CurrentDirectory}\cover.jpg");

            await Task.Delay(1000);
            SendKeys.SendWait("{Enter}");

            var isPostCompleted = await _ISagiriSelenium.PostTwitterAsync();
            if (!isPostCompleted)
            {
                MessageBox.Show("投稿失敗しました。", "投稿失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _Logger.WriteLog("[SagiriUI] - 投稿に失敗しました。", Logger.LogLevel.Error);
                return;
            }

            var sb = new StringBuilder();
            sb.Append("Twitter へ投稿完了しました。\r\n");
            sb.Append("----------------------------\r\n");
            sb.Append($"title: {_CurrentTrackInfo.TrackTitle}\r\n");
            sb.Append($"Artist: {_CurrentTrackInfo.Artist}\r\n");
            sb.Append($"Album: {_CurrentTrackInfo.Album}\r\n");

            new MessageBoxEx(sb.ToString(), "投稿完了！", 3000).Show();

            #endregion Selenium Side

            #region Logging

            _Logger.WriteLog(
                $"Posted on Twitter -> " +
                $"🎵 {_CurrentTrackInfo.TrackTitle} - " +
                $"🎙 {_CurrentTrackInfo.Artist} - " +
                $"💿 {_CurrentTrackInfo.Album}",
                Logger.LogLevel.Info
            );

            #endregion Logging
        }

        private async void MisskeyPostPanel_Click(object sender, EventArgs e)
        {
            var note = new StringBuilder();
            note.Append($"🎵 {_CurrentTrackInfo.TrackTitle}\r\n");
            note.Append($"🎙 {_CurrentTrackInfo.Artist}\r\n");
            note.Append($"💿 {_CurrentTrackInfo.Album}\r\n");
            note.Append("#nowplaying #Spotify #Sagiri");

            var client = _Client.Value;
            var byteStream = await client.GetByteArrayAsync(_CurrentTrackInfo.ArtworkUrl);
            var ps = new Dictionary<string, object> {
                { "text", note.ToString() },
                { "visibility", "home" }
            };

            var form = new MultipartFormDataContent {
                { new ByteArrayContent(byteStream), "file", "cover.jpg" }
            };

            var file = await _IMisskeyService.RequestWithBinaryAsync("drive/files/create", form);
            ps.Add("mediaIds", new string[] { file.id });
            _ = await _IMisskeyService.RequestAsync("notes/create", ps);

            new MessageBoxEx(note.ToString(), "投稿完了！", 3000).Show();

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
            {
                this.Close();
            }
        }

        private void InfoPanel_Click(object sender, EventArgs e) =>
            new InfoWindow(_CurrentTrackInfo).Show();

        private void TitlePanel_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                _MousePoint = new Point(e.X, e.Y);
            }
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
