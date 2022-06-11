﻿using HikiCoffee.ApiIntegration.ProductTranslationAPI;
using HikiCoffee.AppManager.Service;
using HikiCoffee.AppManager.Views.MessageDialogViews;
using HikiCoffee.Models;
using HikiCoffee.Models.DataRequest.ProductTranslations;
using Microsoft.Extensions.DependencyInjection;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace HikiCoffee.AppManager.ViewModels.ProductViewModels
{
    public class UpdateProductTranslationVM : BindableBase
    {
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


        public DelegateCommand UpdateProductTranslationCommand { get; set; }

        private readonly TokenService tokenService;
        private readonly IProductTranslationAPI _productTranslationAPI;

        public UpdateProductTranslationVM(ProductTranslation productTranslation)
        {
            NameProduct = productTranslation.NameProduct;
            Description = productTranslation.Description;
            Details = productTranslation.Details;
            SeoDescription = productTranslation.SeoDescription;
            SeoTitle = productTranslation.SeoTitle;

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDataProtection();
            var service = serviceCollection.BuildServiceProvider();
            tokenService = ActivatorUtilities.GetServiceOrCreateInstance<TokenService>(service);

            _productTranslationAPI = new ProductTranslationAPI();

            UpdateProductTranslationCommand = new DelegateCommand(async () =>
            {
                ProductTranslationUpdateRequest request = new ProductTranslationUpdateRequest()
                {
                    ProductTranslationId = productTranslation.Id,
                    Description = Description,
                    NameProduct = NameProduct,
                    Details = Details,
                    SeoDescription = SeoDescription,
                    SeoTitle = SeoTitle
                };

                try
                {
                    string token = tokenService.ReadToken();

                    string result = await _productTranslationAPI.UpdateProductTranslation(token, request);

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
                if (string.IsNullOrEmpty(NameProduct) || productTranslation == null)
                    return false;

                return true;
            }).ObservesProperty(() => NameProduct);
        }

    }
}
