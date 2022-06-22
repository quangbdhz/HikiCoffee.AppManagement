using HikiCoffee.ApiIntegration.CategoryTranslationAPI;
using HikiCoffee.ApiIntegration.ProductAPI;
using HikiCoffee.ApiIntegration.ProductInCategoryAPI;
using HikiCoffee.ApiIntegration.UnitTranslationAPI;
using HikiCoffee.AppManager.Service.UploadImageCloudinary;
using HikiCoffee.AppManager.Views.MessageDialogViews;
using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Models.DataRequest.Products;
using HikiCoffee.Utilities;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace HikiCoffee.AppManager.ViewModels.ProductViewModels
{
    public class AddProductVM : BindableBase
    {
        private ObservableCollection<CategoryTranslation> _categoryTranslations;
        public ObservableCollection<CategoryTranslation> CategoryTranslations
        {
            get { return _categoryTranslations; }
            set { SetProperty(ref _categoryTranslations, value); }
        }

        private ObservableCollection<CategoryTranslation> _addCategoryInProducts;
        public ObservableCollection<CategoryTranslation> AddCategoryInProducts
        {
            get { return _addCategoryInProducts; }
            set { SetProperty(ref _addCategoryInProducts, value); }
        }

        private ObservableCollection<UnitTranslation> _unitTranslations;
        public ObservableCollection<UnitTranslation> UnitTranslations
        {
            get { return _unitTranslations; }
            set { SetProperty(ref _unitTranslations, value); }
        }

        private string _urlImageCoverProduct;
        public string UrlImageCoverProduct
        {
            get { return _urlImageCoverProduct; }
            set { SetProperty(ref _urlImageCoverProduct, value); }
        }

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }

        private bool _isFeatured;
        public bool IsFeatured
        {
            get { return _isFeatured; }
            set { SetProperty(ref _isFeatured, value); }
        }

        private int _productIdAfterAdd;
        public int ProductIdAfterAdd
        {
            get { return _productIdAfterAdd; }
            set { SetProperty(ref _productIdAfterAdd, value); }
        }

        private UnitTranslation _selectedUnitTranslation;
        public UnitTranslation SelectedUnitTranslation
        {
            get { return _selectedUnitTranslation; }
            set { SetProperty(ref _selectedUnitTranslation, value); }
        }

        public DelegateCommand UpLoadImageProductCommand { get; set; }
        public DelegateCommand<CategoryTranslation> SelectCategoryCommand { get; set; }
        public DelegateCommand<UnitTranslation> SelectedUnitCommand { get; set; }

        public DelegateCommand AddProductCommand { get; set; }

        private readonly IUploadImageCloudinaryService _uploadImageCloudinaryService;
        private readonly IProductAPI _productAPI;
        private readonly ICategoryTranslationAPI _categoryTranslationAPI;
        private readonly IUnitTranslationAPI _unitTranslationAPI;
        private readonly ProductInCategoryAPI _productInCategoryAPI;

        public AddProductVM()
        {
            _productAPI = new ProductAPI();
            _categoryTranslationAPI = new CategoryTranslationAPI();
            _uploadImageCloudinaryService = new UploadImageCloudinaryService();
            _unitTranslationAPI = new UnitTranslationAPI();
            _productInCategoryAPI = new ProductInCategoryAPI();

            Loaded();

            AddProductCommand = new DelegateCommand(async () =>
            {
                ProductCreateRequest request = new ProductCreateRequest() { UrlImageCoverProduct = UrlImageCoverProduct, Price = Price, IsFeatured = IsFeatured, UnitId = SelectedUnitTranslation.UnitId };
                if (request != null)
                {
                    request.UrlImageCoverProduct = await _uploadImageCloudinaryService.UploadImageCloudinary(request.UrlImageCoverProduct);

                    ApiResult<int> result = await _productAPI.AddProduct(request, SystemConstants.TokenInUse);

                    ProductIdAfterAdd = result.ResultObj;

                    foreach (var item in AddCategoryInProducts)
                    {
                        var productInCategory = new ProductInCategory() { CategoryId = item.CategoryId, ProductId = ProductIdAfterAdd };
                        await AddProductIncategory(productInCategory);
                    }

                    MessageDialogView messageDialog = new MessageDialogView(result.Message, 0);
                    messageDialog.Show();
                }
            }, () =>
            {
                if (string.IsNullOrEmpty(UrlImageCoverProduct) || Price == 0)
                    return false;
                return true;
            }).ObservesProperty(() => UrlImageCoverProduct).ObservesProperty(() => Price);

            UpLoadImageProductCommand = new DelegateCommand(() =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        UrlImageCoverProduct = openFileDialog.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageDialogView messageDialogView = new MessageDialogView(ex.Message, 1);
                        messageDialogView.Show();
                    }
                }
            }).ObservesProperty(() => UrlImageCoverProduct);

            SelectCategoryCommand = new DelegateCommand<CategoryTranslation>((p) =>
            {
                if (p != null)
                {
                    if (AddCategoryInProducts.IndexOf(p) == -1)
                        AddCategoryInProducts.Add(p);
                    else
                        AddCategoryInProducts.Remove(p);
                }

            });

            SelectedUnitCommand = new DelegateCommand<UnitTranslation>((p) => { SelectedUnitTranslation = p; });
        }

        private async void Loaded()
        {
            CategoryTranslations = new ObservableCollection<CategoryTranslation>();
            AddCategoryInProducts = new ObservableCollection<CategoryTranslation>();
            UnitTranslations = new ObservableCollection<UnitTranslation>();

            CategoryTranslations = await _categoryTranslationAPI.GetAllCategoryTranslationByLanguageId(SystemConstants.LanguageIdInUse, SystemConstants.TokenInUse);
            UnitTranslations = await _unitTranslationAPI.GetAllUnitTranslation(SystemConstants.LanguageIdInUse, SystemConstants.TokenInUse);
        }

        private async Task<bool> AddProductIncategory(ProductInCategory productInCategory)
        {
            try
            {
                await _productInCategoryAPI.AddProductInCategory(productInCategory, SystemConstants.TokenInUse);
                return true;
            }
            catch (Exception ex)
            {
                MessageDialogView messageDialogView = new MessageDialogView(ex.Message, 1);
                messageDialogView.Show();

                return false;
            }
        }
    }
}
