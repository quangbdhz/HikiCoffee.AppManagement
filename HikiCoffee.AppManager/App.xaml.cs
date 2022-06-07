using HikiCoffee.AppManager.MyUserControl;
using HikiCoffee.AppManager.ViewModels;
using HikiCoffee.AppManager.ViewModels.MainViewModels;
using HikiCoffee.AppManager.ViewModels.MyUserControlViewModels;
using HikiCoffee.AppManager.Views;
using Prism.Ioc;
using Prism.Mvvm;
using System.Windows;

namespace HikiCoffee.AppManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<LoginView>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.Register<ControlBarUC, ControlBarUCVM>();
            ViewModelLocationProvider.Register<MainView, MainVM>();
            ViewModelLocationProvider.Register<LoginView, LoginVM>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
