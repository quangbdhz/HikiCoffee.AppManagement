using HikiCoffee.ApiIntegration.CategoryTranslationAPI;
using HikiCoffee.ApiIntegration.ProductAPI;
using HikiCoffee.AppManager.Service;
using HikiCoffee.AppManager.Service.UploadImageCloudinary;
using HikiCoffee.AppManager.Views.MessageDialogViews;
using HikiCoffee.Models;
using HikiCoffee.Models.DataRequest.Products;
using HikiCoffee.Utilities;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace HikiCoffee.AppManager.ViewModels.ProductViewModels
{


    public class UpdateProductVM : BindableBase
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

        private ObservableCollection<CategoryTranslation> _oldCategoryInProducts;
        public ObservableCollection<CategoryTranslation> OldCategoryInProducts
        {
            get { return _oldCategoryInProducts; }
            set { SetProperty(ref _oldCategoryInProducts, value); }
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
            set
            {
                SetProperty(ref _price, value);
                RaisePropertyChanged(nameof(Price));
            }
        }

        private bool? _isFeatured;
        public bool? IsFeatured
        {
            get { return _isFeatured; }
            set { SetProperty(ref _isFeatured, value); }
        }

        public DelegateCommand<CategoryTranslation> SelectCategoryCommand { get; set; }
        public DelegateCommand UpLoadImageProductCommand { get; set; }
        public DelegateCommand UpdateProductCommand { get; set; }

        private readonly IUploadImageCloudinaryService _uploadImageCloudinaryService;
        private readonly IProductAPI _productAPI;
        private readonly ICategoryTranslationAPI _categoryTranslationAPI;

        public UpdateProductVM(Product? product)
        {
            _categoryTranslationAPI = new CategoryTranslationAPI();
            _uploadImageCloudinaryService = new UploadImageCloudinaryService();
            _productAPI = new ProductAPI();

            AddCategoryInProducts = new ObservableCollection<CategoryTranslation>();
            OldCategoryInProducts = new ObservableCollection<CategoryTranslation>();

            if (product != null)
            {
                UrlImageCoverProduct = product.UrlImageCoverProduct;
                Price = product.Price;
                IsFeatured = product.IsFeatured;

                Loaded(product);
            }

            SelectCategoryCommand = new DelegateCommand<CategoryTranslation>((p) =>
            {
                if (p != null)
                {
                    var search = AddCategoryInProducts.Where(x => x.CategoryId == p.CategoryId).FirstOrDefault();
                    if (search == null)
                    {
                        AddCategoryInProducts.Add(p);
                    }
                    else
                    {
                        AddCategoryInProducts.Remove(search);
                    }
                }

            }).ObservesProperty(() => AddCategoryInProducts);

            UpdateProductCommand = new DelegateCommand(async () =>
            {

                if (product != null)
                {
                    foreach (var item in OldCategoryInProducts)
                    {
                        await _categoryTranslationAPI.DeleteCategoryInProduct(product.Id, item.CategoryId, SystemConstants.TokenInUse);
                    }

                    foreach (var item in AddCategoryInProducts)
                    {

                        var productInCategory = new ProductInCategory() { CategoryId = item.CategoryId, ProductId = product.Id };
                        await _categoryTranslationAPI.AddCategoryInProduct(productInCategory, SystemConstants.TokenInUse);

                    }

                    if(UrlImageCoverProduct != product.UrlImageCoverProduct)
                    {
                        UrlImageCoverProduct = await _uploadImageCloudinaryService.UploadImageCloudinary(UrlImageCoverProduct);
                    }

                    ProductUpdateRequest productUpdateRequest = new ProductUpdateRequest() { Id = product.Id, IsFeatured = IsFeatured, OriginalPrice = product.Price, Price = Price, UrlImageCoverProduct = UrlImageCoverProduct };

                    string response = await _productAPI.UpdateProduct(productUpdateRequest, SystemConstants.TokenInUse);

                    MessageDialogView messageDialogView = new MessageDialogView(response, 0);
                    messageDialogView.Show();
                }
                else
                {
                    MessageDialogView messageDialogView = new MessageDialogView("Product is null", 1);
                    messageDialogView.Show();
                }
            }, () =>
            {
                if (product == null || string.IsNullOrEmpty(UrlImageCoverProduct) || Price < 0)
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
        }

        public async void Loaded(Product product)
        {
            try
            {
                string languageId = Rms.Read("Language", "Id", "");
                if (languageId == "")
                {
                    languageId = "0";
                    Rms.Write("Language", "Id", "0");
                }

                int languageIdDefault = Int32.Parse(languageId);

                CategoryTranslations = await _categoryTranslationAPI.GetAllCategoryTranslationByLanguageId(languageIdDefault, SystemConstants.TokenInUse);

                AddCategoryInProducts = await _categoryTranslationAPI.GetAllCategoryTranslationOfProduct(languageIdDefault, product.Id, SystemConstants.TokenInUse);
                OldCategoryInProducts = await _categoryTranslationAPI.GetAllCategoryTranslationOfProduct(languageIdDefault, product.Id, SystemConstants.TokenInUse); ;
            }
            catch(Exception ex)
            {
                MessageDialogView messageDialogView = new MessageDialogView(ex.Message, 1);
                messageDialogView.Show();
            }
        }

    }
}
