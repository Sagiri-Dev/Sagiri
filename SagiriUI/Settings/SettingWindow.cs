using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using Sagiri.Services.Spotify.Track;
using SagiriUI.Settings;

namespace SagiriUI
{
    public partial class SettingWindow : Form
    {
        #region Properties

        public SettingJsonFile SettingSource { get; set; }

        private Point _MousePoint { get; set; }

        #endregion Properties

        public SettingWindow(SettingJsonFile settingSource)
        {
            this.InitializeComponent();
            this.SettingSource = settingSource;
        }

        #region Event Handlers

        private void SettingWindow_Load(object sender, EventArgs e)
        {
            buttonOk.BackColor = Color.FromArgb(58, 58, 58);

            this.MouseDown += (_, e) => _OnMouseDownEvent(e);
            this.MouseMove += (_, e) => _OnMouseMoveEvent(e);
            TitlePanel.MouseDown += (_, e) => _OnMouseDownEvent(e);
            TitlePanel.MouseMove += (_, e) => _OnMouseMoveEvent(e);
            //BorderPanel.MouseDown += (_, e) => _OnMouseDownEvent(e);
            //BorderPanel.MouseMove += (_, e) => _OnMouseMoveEvent(e);

            this.textBoxPostingFormat.Text = this.SettingSource.PostingFormat;

            try { this.PreviewLabel.Text = this._RenderPreview(); }
            catch { this.PreviewLabel.Text = "(！)投稿フォーマットが無効です"; }
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

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.SettingSource.PostingFormat = this.textBoxPostingFormat.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBoxPostingFormat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.PreviewLabel.Text = this._RenderPreview();
                this.buttonOk.Enabled = true;
            }
            catch
            {
                this.PreviewLabel.Text = "(！)投稿フォーマットが無効です";
                this.buttonOk.Enabled = false;
            }
        }

        private void licenseLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var url = "https://github.com/Sagiri-Dev/Sagiri/blob/feature-misskey-nowplaying/LICENSE";
            var p = new Process()
            {
                StartInfo = new ProcessStartInfo(url) { UseShellExecute = true }
            };
            p.Start();
        }

        #endregion Event Handlers

        #region Private Methods

        private string _RenderPreview()
        {
            CurrentTrackInfo trackInfo = new()
            {
                Album = "メルト 10th ANNIVERSARY MIX",
                Artist = "ryo (supercell) - やなぎなぎ",
                TrackTitle = "メルト 10th ANNIVERSARY MIX",
                TrackNumber = "1",
                ReleaseDate = "2017/12/24",
            };

            return Helper.GenerateTrackText(this.textBoxPostingFormat.Text, trackInfo);
        }

        #endregion Private Methods

        #region Public Methods

        #endregion Public Methods
    }
}
