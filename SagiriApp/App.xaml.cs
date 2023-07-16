using System.Windows;

using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;

using SagiriApp.ViewModel;
using SagiriApp.Views;

namespace SagiriApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell() => Container.Resolve<MainWindow>();

        protected override void RegisterTypes(IContainerRegistry containerRegistry) => 
            containerRegistry.RegisterForNavigation<MainWindow>();

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.Register<MainWindow, SagiriViewModel>();
        }
    }
}
