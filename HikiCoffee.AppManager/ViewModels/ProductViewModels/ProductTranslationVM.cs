using HikiCoffee.ApiIntegration.ProductTranslationAPI;
using HikiCoffee.AppManager.Service;
using HikiCoffee.AppManager.Views.MessageDialogViews;
using HikiCoffee.AppManager.Views.ProductViews.ProductTranslationViews;
using HikiCoffee.Models;
using Microsoft.Extensions.DependencyInjection;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace HikiCoffee.AppManager.ViewModels.ProductViewModels
{
    public class ProductTranslationVM : BindableBase
    {
        private ObservableCollection<Product>? _products;
        public ObservableCollection<Product>? Products
        {
            get { return _products; }
            set { SetProperty(ref _products, value); }
        }

        private ObservableCollection<ProductTranslation> _productTranslations;
        public ObservableCollection<ProductTranslation> ProductTranslations
        {
            get { return _productTranslations; }
            set { SetProperty(ref _productTranslations, value); }
        }

        private string? _urlImageCoverProduct;
        public string? UrlImageCoverProduct
        {
            get { return _urlImageCoverProduct; }
            set { SetProperty(ref _urlImageCoverProduct, value); }
        }

        private string _tokenInUse;
        public string TokenInUse
        {
            get { return _tokenInUse; }
            set { SetProperty(ref _tokenInUse, value); }
        }


        private ProductTranslation _selectedProductTranslation;
        public ProductTranslation SelectedProductTranslation
        {
            get { return _selectedProductTranslation; }
            set { SetProperty(ref _selectedProductTranslation, value); }
        }


        public DelegateCommand AddProductTranslationWindowCommand { get; set; }
        public DelegateCommand UpdateProductTranslationWindowCommand { get; set; }
        public DelegateCommand DeleteProductTranslationWindowCommand { get; set; }

        public DelegateCommand<ProductTranslation> MouseUpLvProductTranslationCommand { get; set; }



        private readonly TokenService tokenService;
        private readonly IProductTranslationAPI _productTranslationAPI;

        public ProductTranslationVM(Product product)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDataProtection();
            var service = serviceCollection.BuildServiceProvider();
            tokenService = ActivatorUtilities.GetServiceOrCreateInstance<TokenService>(service);

            TokenInUse = tokenService.ReadToken();

            Products = new ObservableCollection<Product>();
            Products.Add(product);

            _productTranslationAPI = new ProductTranslationAPI();

            UrlImageCoverProduct = product.UrlImageCoverProduct;

            Loaded(product);


            AddProductTranslationWindowCommand = new DelegateCommand(() =>
            {
                AddProductTranslationView addProductTranslationView = new AddProductTranslationView();
                addProductTranslationView.DataContext = new AddProductTranslationVM(product);
                addProductTranslationView.ShowDialog();

            }, () =>
            {
                if (product.Id == 0 || product == null)
                    return false;
                return true;
            });

            UpdateProductTranslationWindowCommand = new DelegateCommand(() =>
            {
                UpdateProductTranslationView updateProductTranslationView = new UpdateProductTranslationView();
                updateProductTranslationView.DataContext = new UpdateProductTranslationVM(SelectedProductTranslation);
                updateProductTranslationView.ShowDialog();

            }, () =>
            {
                if (SelectedProductTranslation == null)
                    return false;
                return true;
            }).ObservesProperty(() => SelectedProductTranslation);

            DeleteProductTranslationWindowCommand = new DelegateCommand(async () => 
            {
                try
                {
                    var reponse = await _productTranslationAPI.DeleteProductTranslation(SelectedProductTranslation.Id, TokenInUse);

                    ProductTranslations.Remove(SelectedProductTranslation);

                    MessageDialogView messageDialogView = new MessageDialogView(reponse, 0);
                    messageDialogView.Show();
                }
                catch(Exception ex)
                {
                    MessageDialogView messageDialogView = new MessageDialogView(ex.Message, 1);
                    messageDialogView.Show();
                }
            }, () =>
            {
                if (SelectedProductTranslation == null)
                    return false;

                return true;
            }).ObservesProperty(() => SelectedProductTranslation);

            MouseUpLvProductTranslationCommand = new DelegateCommand<ProductTranslation>((p) => { if (p != null) SelectedProductTranslation = p; });
        }

        public async void Loaded(Product product)
        {
            try
            {
                ProductTranslations = await _productTranslationAPI.GetAllProductTranslations(product.Id, TokenInUse);
            }
            catch (Exception ex)
            {
                MessageDialogView messageDialogView = new MessageDialogView(ex.Message, 1);
                messageDialogView.Show();
            }

        }
    }
}
