using HikiCoffee.ApiIntegration.ImportProductAPI;
using HikiCoffee.AppManager.Views.ImportProductViews;
using HikiCoffee.AppManager.Views.MessageDialogViews;
using HikiCoffee.Models;
using HikiCoffee.Utilities;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace HikiCoffee.AppManager.ViewModels.ImportProductViewModels
{
    public class PageImportProductVM : BindableBase
    {
        private ObservableCollection<ImportProduct> _importProducts;
        public ObservableCollection<ImportProduct> ImportProducts
        {
            get { return _importProducts; }
            set { SetProperty(ref _importProducts, value); }
        }

        private ImportProduct? _selectedItemImportProduct;
        public ImportProduct? SelectedItemImportProduct
        {
            get { return _selectedItemImportProduct; }
            set { SetProperty(ref _selectedItemImportProduct, value); }
        }


        public DelegateCommand AddImportProductWindowCommand { get; set; }

        public DelegateCommand DeleteImportProductCommand { get; set; }

        public DelegateCommand<ImportProduct> MouseUpLvImportProductCommand { get; set; }

        
        private readonly IImportProductAPI _importProductAPI;

        public PageImportProductVM()
        {
            _importProductAPI = new ImportProductAPI();

            Loaded();

            AddImportProductWindowCommand = new DelegateCommand(() => 
            { 
                AddImportProductView addImportProductView = new AddImportProductView();
                addImportProductView.ShowDialog();

                Loaded();
            }).ObservesProperty(() => ImportProducts);

            DeleteImportProductCommand = new DelegateCommand(() => { }, () => { return false; });

            MouseUpLvImportProductCommand = new DelegateCommand<ImportProduct>((p) =>
            {
                SelectedItemImportProduct = p;

            });
        }

        public async void Loaded()
        {
            ImportProducts = new ObservableCollection<ImportProduct>();

            try
            {
                var pagingImportProduct = await _importProductAPI.GetAllImportProduct(1, 10, SystemConstants.LanguageIdInUse, SystemConstants.TokenInUse);
                ImportProducts = pagingImportProduct.Items;

            }
            catch(Exception ex)
            {
                MessageDialogView messageDialogView = new MessageDialogView(ex.Message, 1);
                messageDialogView.Show();
            }
        }
    }
}
