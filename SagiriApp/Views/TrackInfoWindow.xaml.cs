using System;

using MahApps.Metro.Controls;

namespace SagiriApp.Views
{
    /// <summary>
    /// TrackInfoWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class TrackInfoWindow : MetroWindow
    {
        private static Lazy<TrackInfoWindow> _Instance = new();
        internal static TrackInfoWindow GetInstance => _Instance.Value;

        public TrackInfoWindow()
        {
            InitializeComponent();
            this.Closing += (_, e) => { e.Cancel = true; this.Hide(); };

            // Must be Relation SagiriViewModel.
            var mainWindow = (MainWindow)App.Current.MainWindow;
            this.DataContext = mainWindow.DataContext;
        }
    }
}
