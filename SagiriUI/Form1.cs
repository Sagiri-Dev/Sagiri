using System;
using System.Diagnostics;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

using Sagiri.Services.Spotify;
using Sagiri.Services.Spotify.Track;
using Sagiri.Util.Common;
using Sagiri.Util.Configuration;
using SagiriUI.Properties;

using Twist;

namespace SagiriUI
{
    public partial class Form1 : Form
    {
        #region Properties

        private SpotifyService _SpotifyService { get; set; }
        private CurrentTrackInfo _CurrentTrackInfo { get; set; }

        private SpotifyCredentialConfig _SpotifyCredentialConfig { get; set; }
        private TwitterCredentialConfig _TwitterCredentialConfig { get; set; }

        private Twitter _Twist { get; set; }
        private Logger _Logger { get; set; }
        private Point _MousePoint { get; set; }

        #endregion Properties

        #region Constructor

        public Form1() => InitializeComponent();

        #endregion Constructor

        #region Private Methods

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

        private void _OnSpotifyCurrentlyPlayingChanged(CurrentTrackInfo trackInfo)
        {
            _CurrentTrackInfo = trackInfo;
            this.Invoke((MethodInvoker)(() => pictureBoxAlbumArt.ImageLocation = trackInfo.ArtworkUrl));
            _Logger.WriteLog("Called OnSpotifyCurrentlyPlayingChanged", Logger.LogLevel.Info);
        }

        private async void NowPlayingPanel_Click(object sender, EventArgs e)
        {
            var tw = new StringBuilder();
            tw.Append($"🎵 {_CurrentTrackInfo.TrackTitle}\r\n");
            tw.Append($"🎙 {_CurrentTrackInfo.Artist}\r\n");
            tw.Append($"💿 {_CurrentTrackInfo.Album}\r\n");
            tw.Append("#nowplaying #Spotify #Sagiri");

            using var client = new System.Net.WebClient();
            using var stream = new System.IO.MemoryStream(client.DownloadData(_CurrentTrackInfo.ArtworkUrl));
            await _Twist.UpdateWithMediaAsync(tw.ToString(), stream);

            _Logger.WriteLog(
                $"Sent Twitter is -> " +
                $"🎵 {_CurrentTrackInfo.TrackTitle} - " +
                $"🎙 {_CurrentTrackInfo.Artist} - " +
                $"💿 {_CurrentTrackInfo.Album}",
                Logger.LogLevel.Info
            );
        }

        private void ClosePanel_Click(object sender, EventArgs e)
        {
            if (!this.IsDisposed && !this.Disposing)
            {
                this.Close();
            }
        }

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

        private void InfoPanel_Click(object sender, EventArgs e) 
            => new InfoWindow(_CurrentTrackInfo).Show();

        private async void AccountPanel_Click(object sender, EventArgs e)
        {
            Process process = new();
            var url = await _SpotifyService.GetUserProfileUrl();
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

        #endregion Private Methods

        #region Public Methods

        public async void Form1_Load(object sender, EventArgs e)
        {
            _Logger = Logger.GetInstance;

            _SpotifyCredentialConfig = SpotifyCredentialConfig.Instance;
            _TwitterCredentialConfig = TwitterCredentialConfig.Instance;

            _SpotifyService = new();
            _CurrentTrackInfo = new();

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
                _Logger.WriteLog("[SagiriUI] - Finish read twitter credential info.", Logger.LogLevel.Info);
            }
            else
            {
                var message = "[SagiriUI] - Twitter tokens file not found...\r\nCan't sent now-playing twitter.";
                MessageBox.Show(message);
                _Logger.WriteLog(message, Logger.LogLevel.Error);
            }

            await _SpotifyService.Initialize();
            if (_SpotifyCredentialConfig.IsExistCredentialFile())
            {
                _SpotifyService.CurrentTrackChanged += _OnSpotifyCurrentlyPlayingChanged;

                await _SpotifyService.Start().ConfigureAwait(false);
                _OnSpotifyCurrentlyPlayingChanged(_CurrentTrackInfo);

                var accountImageStream = await _SpotifyService.GetUserImageStream();
                AccountPanel.BackgroundImage = Image.FromStream(accountImageStream) ?? Resources.account;
            } 
            else
            {
                var message = "[SagiriUI] - Spotify tokens file not found...\r\nClose this app.";
                MessageBox.Show(message);
                _Logger.WriteLog(message, Logger.LogLevel.Fatal);

                this.Close();
            }

            _Logger.WriteLog("[SagiriUI] - Finished roading Form....", Logger.LogLevel.Debug);
        }

        #endregion Public Methods
    }
}
