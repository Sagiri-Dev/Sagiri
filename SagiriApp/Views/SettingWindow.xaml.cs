using System;
using MahApps.Metro.Controls;
using SagiriApp.Interop;

namespace SagiriApp.Views
{
    /// <summary>
    /// SettingWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingWindow : MetroWindow
    {
        private static Lazy<SettingWindow> _Instance = new();
        internal static SettingWindow GetInstance => _Instance.Value;

        public SettingWindow()
        {
            InitializeComponent();

            this.Closing += (_, e) => { e.Cancel = true; this.Hide(); };

            this.Loaded += (_, _) => {
                if (Helper.GetActiveWindow is SettingWindow sw)
                    sw.PreviewText.Text = Helper.RenderPreview(sw.PostingFormatText.Text);
            };

            // Must be Relation SagiriViewModel.
            var mainWindow = (MainWindow)App.Current.MainWindow;
            this.DataContext = mainWindow.DataContext;
        }
    }
}
