using HikiCoffee.AppManager.MyUserControl;
using HikiCoffee.AppManager.ViewModels;
using HikiCoffee.AppManager.ViewModels.BillViewModels;
using HikiCoffee.AppManager.ViewModels.CategoryViewModels;
using HikiCoffee.AppManager.ViewModels.CoffeeTableViewModels;
using HikiCoffee.AppManager.ViewModels.ImportProductViewModels;
using HikiCoffee.AppManager.ViewModels.MainViewModels;
using HikiCoffee.AppManager.ViewModels.MyUserControlViewModels;
using HikiCoffee.AppManager.ViewModels.ProductViewModels;
using HikiCoffee.AppManager.ViewModels.SuplierViewModels;
using HikiCoffee.AppManager.ViewModels.UserViewModels;
using HikiCoffee.AppManager.Views;
using HikiCoffee.AppManager.Views.CoffeeTableViews;
using HikiCoffee.AppManager.Views.ImportProductViews;
using HikiCoffee.AppManager.Views.MainViews.Pages;
using HikiCoffee.AppManager.Views.ProductViews;
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
            ViewModelLocationProvider.Register<AddProductView, AddProductVM>();
            ViewModelLocationProvider.Register<ImportProductPage, PageImportProductVM>();
            ViewModelLocationProvider.Register<AddImportProductView, AddImportProductVM>();
            ViewModelLocationProvider.Register<CustomerPage, PageCustomerVM>();

            ViewModelLocationProvider.Register<CoffeeTablePage, PageCoffeeTableVM>();
            ViewModelLocationProvider.Register<MoveCoffeeTableView, MoveCoffeeTableVM>();
            ViewModelLocationProvider.Register<MergeCoffeeTableView, MergeCoffeeTableVM>();

            ViewModelLocationProvider.Register<CategoryPage, PageCategoryVM>();

            ViewModelLocationProvider.Register<SuplierPage, PageSuplierVM>();

            ViewModelLocationProvider.Register<BillPage, PageBillVM>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
