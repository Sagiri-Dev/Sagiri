using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using Microsoft.Xaml.Behaviors;

using Sagiri.Services.Spotify.Track;
using Sagiri.Util.Common;
using SagiriApp.Interop;
using SagiriApp.Views;

namespace SagiriApp.Behavior
{
    class PostingFormatTextBehavior : Behavior<TextBox>
    {
        private Logger _Logger { get; set; } = Logger.GetInstance;
        public string PostingFormat
        {
            get => (string)GetValue(PostingFormatProperty);
            set => SetValue(PostingFormatProperty, value);
        }

        // Using a DependencyProperty as the backing store for PostingFormat. This enables binding.
        public static readonly DependencyProperty PostingFormatProperty =
            DependencyProperty.Register(
                "PostingFormat",
                typeof(string),
                typeof(PostingFormatTextBehavior),
                new PropertyMetadata(default(string)
            )
        );

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.TextChanged += _OnTextChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.TextChanged -= this._OnTextChanged;
        }

        private void _OnTextChanged(object sender, EventArgs e)
        {
            var activeWindow = Application.Current.Windows
                .OfType<Window>()
                .SingleOrDefault(x => x.IsActive);

            if (activeWindow is SettingWindow sw)
            {
                try
                {
                    PostingFormat = sw.PostingFormatText.Text;
                    sw.PreviewText.Text = _RenderPreview(sw);
                    sw.SettingSave.IsEnabled = true;
                }
                catch
                {
                    sw.PreviewText.Text = "(！)投稿フォーマットが無効です";
                    sw.SettingSave.IsEnabled = false;
                }
            }
        }

        private string _RenderPreview(SettingWindow sw)
        {
            CurrentTrackInfo trackInfo = new()
            {
                Album = "メルト 10th ANNIVERSARY MIX",
                Artist = "ryo (supercell) - やなぎなぎ",
                TrackTitle = "メルト 10th ANNIVERSARY MIX",
                TrackNumber = "1",
                ReleaseDate = "2017/12/24",
            };

            return Helper.GenerateTrackText(sw.PostingFormatText.Text, trackInfo);
        }
    }
}
