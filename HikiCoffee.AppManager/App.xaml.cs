using HikiCoffee.AppManager.MyUserControl;
using HikiCoffee.AppManager.ViewModels;
using HikiCoffee.AppManager.ViewModels.MainViewModels;
using HikiCoffee.AppManager.ViewModels.MyUserControlViewModels;
using HikiCoffee.AppManager.ViewModels.ProductViewModels;
using HikiCoffee.AppManager.Views;
using HikiCoffee.AppManager.Views.MainViews.Pages;
using HikiCoffee.AppManager.Views.ProductViews;
using HikiCoffee.AppManager.Views.ProductViews.ProductTranslationViews;
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

            ViewModelLocationProvider.Register<ProductPage, PageProductVM>();
            ViewModelLocationProvider.Register<AddProductView, PageProductVM>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
