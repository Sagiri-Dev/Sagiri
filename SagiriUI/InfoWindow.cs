using System;
using System.Drawing;
using System.Windows.Forms;

using Sagiri.Services.Spotify.Track;

namespace SagiriUI
{
    public partial class InfoWindow : Form
    {
        private CurrentTrackInfo _CurrentTrackInfo { get; set; }
        private string _Title { get; set; }
        private string _Artist { get; set; }
        private string _Album { get; set; }
        private string _ReleaseDate { get; set; }
        private string _TrackId { get; set; }
        private string _TrackDuration { get; set; }
        private string _PreviewUrl { get; set; }

        private Point _MousePoint { get; set; }

        public InfoWindow(CurrentTrackInfo currentTrackInfo)
        {
            InitializeComponent();
            _CurrentTrackInfo = currentTrackInfo;
        }

        private void InfoWindow_Load(object sender, EventArgs e)
        {
            _Title = _CurrentTrackInfo.TrackTitle;
            _Artist = _CurrentTrackInfo.Artist;
            _Album = _CurrentTrackInfo.Album;
            _ReleaseDate = _CurrentTrackInfo.ReleaseDate;
            _TrackId = _CurrentTrackInfo.TrackId;
            _TrackDuration = _CurrentTrackInfo.TrackDuration;
            _PreviewUrl = _CurrentTrackInfo.PreviewUrl;

            TitleLabel.Text = $"Title : {_Title}";
            ArtistLabel.Text = $"Artist : {_Artist}";
            AlbumLabel.Text = $"Album : {_Album}";
            DurationLabel.Text = $"Duration : { _TrackDuration}";
            ReleaseDateLabel.Text = $"ReleaseDate : {_ReleaseDate}";
            PreviewUrlLabel.Text = $"PreviewUrl : {_PreviewUrl}";

            this.MouseDown += (_, e) => _OnMouseDownEvent(e);
            this.MouseMove += (_, e) => _OnMouseMoveEvent(e);
            TitlePanel.MouseDown += (_, e) => _OnMouseDownEvent(e);
            TitlePanel.MouseMove += (_, e) => _OnMouseMoveEvent(e);
            BorderPanel.MouseDown += (_, e) => _OnMouseDownEvent(e);
            BorderPanel.MouseMove += (_, e) => _OnMouseMoveEvent(e);
        }

        private void ClosePanel_Click(object sender, EventArgs e)
        {
            if (!this.IsDisposed && !this.Disposing)
            {
                this.Close();
                this.Dispose();
            }
        }

        private void _OnMouseDownEvent(MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                _MousePoint = new Point(e.X, e.Y);
        }

        private void _OnMouseMoveEvent(MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Left += e.X - _MousePoint.X;
                this.Top += e.Y - _MousePoint.Y;
            }
        }
    }
}
