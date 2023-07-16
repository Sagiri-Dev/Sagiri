using System;

using MahApps.Metro.Controls;

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

            // Must be Relation SagiriViewModel.
            var mainWindow = (MainWindow)App.Current.MainWindow;
            this.DataContext = mainWindow.DataContext;
        }
    }
}
