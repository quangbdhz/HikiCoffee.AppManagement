using HikiCoffee.ApiIntegration.LanguageAPI;
using HikiCoffee.ApiIntegration.ProductTranslationAPI;
using HikiCoffee.AppManager.Service;
using HikiCoffee.AppManager.Views.MessageDialogViews;
using HikiCoffee.Models;
using HikiCoffee.Models.DataRequest.ProductTranslations;
using Microsoft.Extensions.DependencyInjection;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace HikiCoffee.AppManager.ViewModels.ProductViewModels
{
    public class AddProductTranslationVM : BindableBase
    {

        private ObservableCollection<Language> _languages;
        public ObservableCollection<Language> Languages
        {
            get { return _languages; }
            set { SetProperty(ref _languages, value); }
        }


        private string _nameProduct;
        public string NameProduct
        {
            get { return _nameProduct; }
            set { SetProperty(ref _nameProduct, value); }
        }

        private string? _description;
        public string? Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string? _details;
        public string? Details
        {
            get { return _details; }
            set { SetProperty(ref _details, value); }
        }

        private string? _seoDescription;
        public string? SeoDescription
        {
            get { return _seoDescription; }
            set { SetProperty(ref _seoDescription, value); }
        }

        private string? _seoTitle;
        public string? SeoTitle
        {
            get { return _seoTitle; }
            set { SetProperty(ref _seoTitle, value); }
        }

        private int _languageId;
        public int LanguageId
        {
            get { return _languageId; }
            set { SetProperty(ref _languageId, value); }
        }


        public DelegateCommand<Language> SelectLanguageCommand { get; set; }

        public DelegateCommand AddProductTranslationCommand { get; set; }

        private readonly TokenService tokenService;
        private readonly ILanguageAPI _languageAPI;
        private readonly IProductTranslationAPI _productTranslationAPI;

        public AddProductTranslationVM(Product product)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDataProtection();
            var service = serviceCollection.BuildServiceProvider();
            tokenService = ActivatorUtilities.GetServiceOrCreateInstance<TokenService>(service);

            _languageAPI = new LanguageAPI();
            _productTranslationAPI = new ProductTranslationAPI();

            Loaded();

            SelectLanguageCommand = new DelegateCommand<Language>((p) =>
            {
                if (p != null)
                    LanguageId = p.Id;
            });

            AddProductTranslationCommand = new DelegateCommand(async () =>
            {
                ProductTranslationCreateRequest request = new ProductTranslationCreateRequest()
                {
                    ProductId = product.Id,
                    Description = Description,
                    NameProduct = NameProduct,
                    Details = Details,
                    LanguageId = LanguageId,
                    SeoDescription = SeoDescription,
                    SeoTitle = SeoTitle
                };

                try
                {
                    string token = tokenService.ReadToken();

                    string result = await _productTranslationAPI.AddProductTranslation(token, request);

                    MessageDialogView messageDialogView = new MessageDialogView(result, 0);
                    messageDialogView.Show();
                }
                catch (Exception ex)
                {
                    MessageDialogView messageDialogView = new MessageDialogView(ex.Message, 1);
                    messageDialogView.Show();
                }


            }, () =>
            {
                if (LanguageId == 0 || string.IsNullOrEmpty(NameProduct) || product == null)
                    return false;

                return true;
            }).ObservesProperty(() => LanguageId).ObservesProperty(() => NameProduct);
        }

        public async void Loaded()
        {

            Languages = await _languageAPI.GetAllLanguages(null);
        }
    }
}
