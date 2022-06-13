using HikiCoffee.ApiIntegration.ProductTranslationAPI;
using HikiCoffee.ApiIntegration.SuplierAPI;
using HikiCoffee.AppManager.Views.MessageDialogViews;
using HikiCoffee.Models;
using HikiCoffee.Utilities;
using Prism.Mvvm;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using HikiCoffee.Models.DataRequest.ImportProducts;
using HikiCoffee.ApiIntegration.ImportProductAPI;

namespace HikiCoffee.AppManager.ViewModels.ImportProductViewModels
{
    public class AddImportProductVM : BindableBase
    {
        private ObservableCollection<ProductTranslation> _productTranslations;
        public ObservableCollection<ProductTranslation> ProductTranslations
        {
            get { return _productTranslations; }
            set { SetProperty(ref _productTranslations, value); }
        }

        private ObservableCollection<Suplier> _suplier;
        public ObservableCollection<Suplier> Supliers
        {
            get { return _suplier; }
            set { SetProperty(ref _suplier, value); }
        }

        private ProductTranslation _selectedProductTranslation;
        public ProductTranslation SelectedProductTranslation
        {
            get { return _selectedProductTranslation; }
            set { SetProperty(ref _selectedProductTranslation, value); }
        }

        private Suplier _selectedSuplier;
        public Suplier SelectedSuplier
        {
            get { return _selectedSuplier; }
            set { SetProperty(ref _selectedSuplier, value); }
        }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set { SetProperty(ref _quantity, value); }
        }

        private int _priceImportProduct;
        public int PriceImportProduct
        {
            get { return _priceImportProduct; }
            set { SetProperty(ref _priceImportProduct, value); }
        }

        public DelegateCommand AddImportProductCommand { get; set; }

        private readonly IProductTranslationAPI _productTranslationAPI;
        private readonly ISuplierAPI _suplierAPI;
        private readonly IImportProductAPI _importProductAPI;

        public AddImportProductVM()
        {
            _productTranslationAPI = new ProductTranslationAPI();
            _suplierAPI = new SuplierAPI();
            _importProductAPI = new ImportProductAPI();

            Loaded();

            AddImportProductCommand = new DelegateCommand(async () =>
            {
                var request = new ImportProductCreateRequest() { Quantity = Quantity, PriceImportProduct = PriceImportProduct, ProductId = SelectedProductTranslation.ProductId, SuplierId = SelectedSuplier.Id, UserIdImportProduct = SystemConstants.UserIdInUse };

                var reponse = await _importProductAPI.AddImportProduct(request, SystemConstants.TokenInUse);

                MessageDialogView messageDialogView = new MessageDialogView(reponse, 0);
                messageDialogView.Show();


            }, () =>
            {
                if (PriceImportProduct <= 0 || Quantity < 0)
                    return false;

                if (SelectedProductTranslation == null || SelectedSuplier == null)
                    return false;

                return true;
            }).ObservesProperty(() => PriceImportProduct).ObservesProperty(() => Quantity).ObservesProperty(() => SelectedProductTranslation).ObservesProperty(() => SelectedSuplier);
        }

        public async void Loaded()
        {
            ProductTranslations = new ObservableCollection<ProductTranslation>();
            Supliers = new ObservableCollection<Suplier>();

            try
            {
                ProductTranslations = await _productTranslationAPI.GetAllProductTranslationByLanguageId(SystemConstants.LanguageIdInUse, SystemConstants.TokenInUse);
                Supliers = await _suplierAPI.GetAllSupliers(SystemConstants.TokenInUse);
            }
            catch (Exception ex)
            {
                MessageDialogView messageDialogView = new MessageDialogView(ex.Message, 1);
                messageDialogView.Show();
            }

        }
    }
}
