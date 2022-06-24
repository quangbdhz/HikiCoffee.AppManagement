using HikiCoffee.ApiIntegration.ProductAPI;
using HikiCoffee.AppManager.Views.MessageDialogViews;
using HikiCoffee.AppManager.Views.ProductViews;
using HikiCoffee.AppManager.Views.ProductViews.ProductTranslationViews;
using HikiCoffee.AppManager.Views.ProductViews.ProductViews;
using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Utilities;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace HikiCoffee.AppManager.ViewModels.ProductViewModels
{
    public class PageProductVM : BindableBase, INotifyPropertyChanged
    {
        private ObservableCollection<Product>? _products;
        public ObservableCollection<Product>? Products
        {
            get { return _products; }
            set { SetProperty(ref _products, value); }
        }

        private Product? _selectedItemProduct;
        public Product? SelectedItemProduct
        {
            get { return _selectedItemProduct; }
            set { SetProperty(ref _selectedItemProduct, value); }
        }

        public DelegateCommand AddProductWindowCommand { get; set; }
        public DelegateCommand UpdateProductWindowCommand { get; set; }
        public DelegateCommand DeleteProductCommand { get; set; }

        public DelegateCommand<Product> MouseUpLvProductCommand { get; set; }
        public DelegateCommand<Product> MouseDoubleLvProductCommand { get; set; }

        private readonly IProductAPI _productAPI;

        public PageProductVM()
        {
            Products = new ObservableCollection<Product>();

            _productAPI = new ProductAPI();

            LoadedPageProduct();

            AddProductWindowCommand = new DelegateCommand(() =>
            {

                AddProductView addProductView = new AddProductView();
                addProductView.ShowDialog();
            });

            UpdateProductWindowCommand = new DelegateCommand(() =>
            {
                UpdateProductView updateProductView = new UpdateProductView();
                updateProductView.DataContext = new UpdateProductVM(SelectedItemProduct);
                updateProductView.ShowDialog();

            }, CanExecuteUpdateProductCommand).ObservesProperty(() => SelectedItemProduct);



            MouseUpLvProductCommand = new DelegateCommand<Product>((p) =>
            {
                SelectedItemProduct = p;

            });

            MouseDoubleLvProductCommand = new DelegateCommand<Product>((p) =>
            {
                SelectedItemProduct = p;

                ProductTranslationView productTranslationView = new ProductTranslationView();
                productTranslationView.DataContext = new ProductTranslationVM(p);
                productTranslationView.ShowDialog();

            });

            DeleteProductCommand = new DelegateCommand(async () =>
            {
                try
                {
                    if (SelectedItemProduct != null)
                    {
                        ApiResult<bool> result = await _productAPI.DeleteProduct(SelectedItemProduct.Id, SystemConstants.TokenInUse);

                        if (result.IsSuccessed)
                        {
                            MessageDialogView messageDialogView = new MessageDialogView(result.Message, 0);
                            messageDialogView.Show();
                        }
                        else
                        {
                            MessageDialogView messageDialogView = new MessageDialogView(result.Message, 1);
                            messageDialogView.Show();
                        }
                    }
                    else
                    {
                        MessageDialogView messageDialogView = new MessageDialogView("SelectedItemProduc is null.", 1);
                        messageDialogView.Show();
                    }
                }
                catch (Exception ex)
                {
                    MessageDialogView messageDialogView = new MessageDialogView(ex.Message, 1);
                    messageDialogView.Show();
                }

            }, () =>
            {
                if (SelectedItemProduct == null)
                    return false;
                return true;
            }).ObservesProperty(() => SelectedItemProduct);
        }

        private bool CanExecuteUpdateProductCommand()
        {
            if (SelectedItemProduct == null)
                return false;

            return true;
        }

        private async void LoadedPageProduct()
        {
            try
            {
                PagedResult<Product> pageProductResult = await _productAPI.GetAllProducts(1, 20, SystemConstants.TokenInUse);

                Products = pageProductResult.Items;
            }
            catch (Exception ex)
            {
                MessageDialogView messageDialogView = new MessageDialogView(ex.Message, 1);
                messageDialogView.Show();
            }
        }





    }
}
