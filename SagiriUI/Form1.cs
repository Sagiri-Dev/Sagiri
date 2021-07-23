using System;
using System.Diagnostics;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

using Sagiri.Services.Spotify;
using Sagiri.Services.Spotify.Track;
using SagiriUI.Properties;
using Twist;

namespace SagiriUI
{
    public partial class Form1 : Form
    {
        private SpotifyService _SpotifyService { get; set; }
        private CurrentTrackInfo _CurrentTrackInfo { get; set; }

        // TODO : Token情報の外だし
        private static readonly string _ck = "";
        private static readonly string _cs = "";
        private static readonly string _at = "";
        private static readonly string _ats = "";

        private Twitter _Twist { get; set; }
        private Point _MousePoint { get; set; }

        public Form1() => InitializeComponent();

        public async void Form1_Load(object sender, EventArgs e)
        {
            _SpotifyService = new();
            _CurrentTrackInfo = new CurrentTrackInfo();
            _Twist = new Twitter(_ck, _cs, _at, _ats, new HttpClient(new HttpClientHandler()));

            _SpotifyService.CurrentTrackChanged += _OnSpotifyCurrentlyPlayingChanged;

            if (_SpotifyService.IsExistCredentialFile())
            {
                await _SpotifyService.Initialize();
                await _SpotifyService.Start().ConfigureAwait(false);
            }

            // 初回のみ明示的に初期化
            _OnSpotifyCurrentlyPlayingChanged(_CurrentTrackInfo);

            var accountImageStream = await _SpotifyService.GetUserImageStream();
            AccountPanel.BackgroundImage = Image.FromStream(accountImageStream) ?? Resources.account;
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

        private void _OnSpotifyCurrentlyPlayingChanged(CurrentTrackInfo trackInfo)
        {
            _CurrentTrackInfo = trackInfo;
            this.Invoke((MethodInvoker)(() => pictureBoxAlbumArt.ImageLocation = trackInfo.ArtworkUrl));
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

        private void InfoPanel_Click(object sender, EventArgs e) => new InfoWindow(_CurrentTrackInfo).Show();

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
                throw new Exception($"Unable to open a browser. Please manually open: {url}");
            }
        }
    }
}
