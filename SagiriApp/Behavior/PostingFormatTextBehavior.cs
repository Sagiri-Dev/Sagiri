using System;
using System.Windows;
using System.Windows.Controls;

using Microsoft.Xaml.Behaviors;

using SagiriApp.Interop;
using SagiriApp.Views;

namespace SagiriApp.Behavior
{
    class PostingFormatTextBehavior : Behavior<TextBox>
    {
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
            if (Helper.GetActiveWindow is SettingWindow sw)
            {
                try
                {
                    PostingFormat = sw.PostingFormatText.Text;
                    sw.PreviewText.Text = Helper.RenderPreview(sw.PostingFormatText.Text);
                    sw.SettingSave.IsEnabled = true;
                }
                catch
                {
                    sw.PreviewText.Text = "(！)投稿フォーマットが無効です";
                    sw.SettingSave.IsEnabled = false;
                }
            }
        }
    }
}
