using HikiCoffee.ApiIntegration.CategoryTranslationAPI;
using HikiCoffee.ApiIntegration.ProductAPI;
using HikiCoffee.AppManager.ModelDataRequest.Products;
using HikiCoffee.AppManager.Service;
using HikiCoffee.AppManager.Service.UploadImageCloudinary;
using HikiCoffee.AppManager.Views.MessageDialogViews;
using HikiCoffee.AppManager.Views.ProductViews;
using HikiCoffee.AppManager.Views.ProductViews.ProductTranslationViews;
using HikiCoffee.AppManager.Views.ProductViews.ProductViews;
using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

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

        private ObservableCollection<CategoryTranslation>? _categoryTranslations;
        public ObservableCollection<CategoryTranslation>? CategoryTranslations
        {
            get { return _categoryTranslations; }
            set { SetProperty(ref _categoryTranslations, value); }
        }

        private ObservableCollection<CategoryTranslation>? _addCategoryInProducts;
        public ObservableCollection<CategoryTranslation>? AddCategoryInProducts
        {
            get { return _addCategoryInProducts; }
            set { SetProperty(ref _addCategoryInProducts, value); }
        }

        private Product? _selectedItemProduct;
        public Product? SelectedItemProduct
        {
            get { return _selectedItemProduct; }
            set { SetProperty(ref _selectedItemProduct, value); }
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

        public DelegateCommand AddProductWindowCommand { get; set; }
        public DelegateCommand UpLoadImageProductCommand { get; set; }
        public DelegateCommand AddProductCommand { get; set; }
        public DelegateCommand<CategoryTranslation> SelectCategoryCommand { get; set; }

        public DelegateCommand UpdateProductWindowCommand { get; set; }
        public DelegateCommand DeleteProductCommand { get; set; }

        public DelegateCommand<Product> MouseUpLvProductCommand { get; set; }
        public DelegateCommand<Product> MouseDoubleLvProductCommand { get; set; }

        private readonly TokenService tokenService;
        private readonly IUploadImageCloudinaryService _uploadImageCloudinaryService;
        private readonly IProductAPI _productAPI;
        private readonly ICategoryTranslationAPI _categoryTranslationAPI;

        public PageProductVM()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDataProtection();
            var service = serviceCollection.BuildServiceProvider();
            tokenService = ActivatorUtilities.GetServiceOrCreateInstance<TokenService>(service);

            Products = new ObservableCollection<Product>();
            CategoryTranslations = new ObservableCollection<CategoryTranslation>();
            AddCategoryInProducts = new ObservableCollection<CategoryTranslation>();

            _productAPI = new ProductAPI();
            _categoryTranslationAPI = new CategoryTranslationAPI();
            _uploadImageCloudinaryService = new UploadImageCloudinaryService();

            LoadedPageProduct();
            LoadedAddProductWindow();

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

            SelectCategoryCommand = new DelegateCommand<CategoryTranslation>((p) =>
            {
                if (p != null)
                {
                    if (AddCategoryInProducts.IndexOf(p) == -1)
                    {
                        AddCategoryInProducts.Add(p);
                    }
                    else
                    {
                        AddCategoryInProducts.Remove(p);
                    }
                }

            });

            AddProductCommand = new DelegateCommand(async () =>
            {
                ProductCreateRequest request = new ProductCreateRequest() { UrlImageCoverProduct = UrlImageCoverProduct, Price = Price, IsFeatured = IsFeatured };

                Product newProduct = new Product();

                if (request != null)
                {
                    request.UrlImageCoverProduct = await _uploadImageCloudinaryService.UploadImageCloudinary(request.UrlImageCoverProduct);

                    newProduct = new Product()
                    {
                        UrlImageCoverProduct = request.UrlImageCoverProduct,
                        Price = request.Price,
                        OriginalPrice = request.Price,
                        Stock = 0,
                        ViewCount = 0,
                        DateCreated = DateTime.Now,
                        IsActive = true,
                        IsFeatured = request.IsFeatured
                    };
                }


                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = SystemConstants.DomainName + "/api/Products/Add";

                using (var client = new HttpClient())
                {
                    try
                    {
                        string token = tokenService.ReadToken();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        var response = await client.PostAsync(url, data);

                        var body = await response.Content.ReadAsStringAsync();

                        if (response.IsSuccessStatusCode)
                        {
                            int myDeserializedObjList = Convert.ToInt32(JsonConvert.DeserializeObject(body, typeof(int)));

                            if (myDeserializedObjList != 0)
                            {
                                ProductIdAfterAdd = myDeserializedObjList;
                            }

                            foreach (var item in AddCategoryInProducts)
                            {
                                var productInCategory = new ProductInCategory() { CategoryId = item.CategoryId, ProductId = ProductIdAfterAdd };
                                AddProductIncategory(productInCategory, token);
                            }

                            Products.Add(newProduct);
                            MessageDialogView messageDialog = new MessageDialogView("Add Product is success.", 0);
                            messageDialog.Show();
                        }
                        else
                        {
                            MessageDialogView messageDialogView = new MessageDialogView(body, 1);
                            messageDialogView.Show();
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        MessageDialogView messageDialogView = new MessageDialogView(ex.Message, 1);
                        messageDialogView.Show();
                    }
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
                        string reponse = await _productAPI.DeleteProduct(SelectedItemProduct.Id, SystemConstants.TokenInUse);

                        MessageDialogView messageDialogView = new MessageDialogView(reponse, 0);
                        messageDialogView.Show();
                    }
                    else
                    {
                        MessageDialogView messageDialogView = new MessageDialogView("SelectedItemProduc is null.", 1);
                        messageDialogView.Show();
                    }
                }
                catch(Exception ex)
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
            string token = tokenService.ReadToken();
            try
            {
                PagedResult<Product> pageProductResult = await _productAPI.GetAllProducts(1, 20, token);

                Products = pageProductResult.Items;
            }
            catch (Exception ex)
            {
                MessageDialogView messageDialogView = new MessageDialogView(ex.Message, 1);
                messageDialogView.Show();
            }
        }

        private async void LoadedAddProductWindow()
        {
            string token = tokenService.ReadToken();

            string languageId = Rms.Read("Language", "Id", "");
            if (languageId == "")
            {
                languageId = "0";
                Rms.Write("Language", "Id", "0");
            }

            int languageIdDefault = Int32.Parse(languageId);

            try
            {
                CategoryTranslations = await _categoryTranslationAPI.GetAllCategoryTranslationByLanguageId(languageIdDefault + 1, token);
            }
            catch (Exception ex)
            {
                MessageDialogView messageDialogView = new MessageDialogView(ex.Message, 1);
                messageDialogView.Show();
            }
        }

        private async void AddProductIncategory(ProductInCategory productInCategory, string token)
        {
            var json = JsonConvert.SerializeObject(productInCategory);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = SystemConstants.DomainName + "/api/ProductInCategories/Add";

            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = await client.PostAsync(url, data);

                    var body = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {

                    }
                    else
                    {
                        MessageDialogView messageDialogView = new MessageDialogView(body, 1);
                        messageDialogView.Show();
                    }
                }
                catch (HttpRequestException ex)
                {
                    MessageDialogView messageDialogView = new MessageDialogView(ex.Message, 1);
                    messageDialogView.Show();
                }

            }
        }

    }
}
