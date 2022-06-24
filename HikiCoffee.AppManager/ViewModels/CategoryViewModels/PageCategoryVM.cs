using HikiCoffee.ApiIntegration.CategoryAPI;
using HikiCoffee.ApiIntegration.CategoryTranslationAPI;
using HikiCoffee.AppManager.Service.UploadImageCloudinary;
using HikiCoffee.AppManager.ViewModels.CategoryViewModels.CategoryTranslationViewModels;
using HikiCoffee.AppManager.Views.CategoryViews.CategoryTranslationViews;
using HikiCoffee.AppManager.Views.MessageDialogViews;
using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Models.DataRequest.Categories;
using HikiCoffee.Utilities;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace HikiCoffee.AppManager.ViewModels.CategoryViewModels
{
    public class PageCategoryVM : BindableBase
    {
        #region category
        private ObservableCollection<Category> _categories;
        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }

        private Category? _selectedCategory;
        public Category? SelectedCategory
        {
            get { return _selectedCategory; }
            set { SetProperty(ref _selectedCategory, value); }
        }

        private bool? _isShowOnHome;
        public bool? IsShowOnHome
        {
            get { return _isShowOnHome; }
            set { SetProperty(ref _isShowOnHome, value); }
        }

        private string _urlImageCoverCategory;
        public string UrlImageCoverCategory
        {
            get { return _urlImageCoverCategory; }
            set { SetProperty(ref _urlImageCoverCategory, value); }
        }

        public DelegateCommand<Category> GetSelectedCategoryCommand { get; set; }

        public DelegateCommand AddCategoryCommand { get; set; }

        public DelegateCommand UpLoadImageCategoryCommand { get; set; }

        public DelegateCommand UpdateCategoryCommand { get; set; }

        public DelegateCommand DeleteCategoryCommand { get; set; }
        #endregion

        #region category translation
        private ObservableCollection<CategoryTranslation> _categoryTranslations;
        public ObservableCollection<CategoryTranslation> CategoryTranslations
        {
            get { return _categoryTranslations; }
            set { SetProperty(ref _categoryTranslations, value); }
        }

        private ObservableCollection<CategoryTranslation> _parentIdFilterByCategoryTranslations;
        public ObservableCollection<CategoryTranslation> ParentIdFilterByCategoryTranslations
        {
            get { return _parentIdFilterByCategoryTranslations; }
            set { SetProperty(ref _parentIdFilterByCategoryTranslations, value); }
        }

        private CategoryTranslation? _selectedParentCategory;
        public CategoryTranslation? SelectedParentCategory
        {
            get { return _selectedParentCategory; }
            set { SetProperty(ref _selectedParentCategory, value); }
        }

        private CategoryTranslation? _selectedCategoryTranslation;
        public CategoryTranslation? SelectedCategoryTranslation
        {
            get { return _selectedCategoryTranslation; }
            set { SetProperty(ref _selectedCategoryTranslation, value); }
        }
        
        private string? _categoryIdChoosed;
        public string? CategoryIdChoosed
        {
            get { return _categoryIdChoosed; }
            set { SetProperty(ref _categoryIdChoosed, value); }
        }

        public DelegateCommand AddCategoryTranslationViewCommand { get; set; }

        public DelegateCommand UpdateCategoryTranslationViewCommand { get; set; }

        public DelegateCommand DeleteCategoryTranslationCommand { get; set; }
        #endregion

        private readonly IUploadImageCloudinaryService _uploadImageCloudinaryService;
        private readonly ICategoryAPI _categoryAPI;
        private readonly ICategoryTranslationAPI _categoryTranslationAPI;

        public PageCategoryVM()
        {
            _uploadImageCloudinaryService = new UploadImageCloudinaryService();
            _categoryAPI = new CategoryAPI();
            _categoryTranslationAPI = new CategoryTranslationAPI();

            Loaded();

            #region category command
            GetSelectedCategoryCommand = new DelegateCommand<Category>(async (p) =>
            {
                if (p != null)
                {
                    SelectedCategory = p;

                    IsShowOnHome = p.IsShowOnHome;
                    UrlImageCoverCategory = p.UrlImageCoverCategory != null ? p.UrlImageCoverCategory : "";

                    CategoryIdChoosed = "Category Translation: '" + p.Id + "'";
                    CategoryTranslations = await _categoryTranslationAPI.GetByCategoryId(p.Id, SystemConstants.TokenInUse);
                }
            });

            AddCategoryCommand = new DelegateCommand(async () =>
            {
                UrlImageCoverCategory = await _uploadImageCloudinaryService.UploadImageCloudinary(UrlImageCoverCategory);

                CategoryCreateRequest request = new CategoryCreateRequest() { IsShowOnHome = IsShowOnHome, UrlImageCoverCategory = UrlImageCoverCategory, ParentId = SelectedParentCategory != null ? SelectedParentCategory.Id : null };

                var result = await _categoryAPI.AddCategory(request, SystemConstants.TokenInUse);

                if (!result.IsSuccessed)
                {
                    MessageDialogView messageDialogView = new MessageDialogView(result.Message, 1);
                    messageDialogView.Show();
                }
                else
                {
                    PagedResult<Category> pageCategory = await _categoryAPI.GetAllCategories(1, 20, SystemConstants.TokenInUse);
                    Categories = pageCategory.Items;

                    SetVariableNull();

                    MessageDialogView messageDialogView = new MessageDialogView(result.Message, 0);
                    messageDialogView.Show();
                }
            }, () =>
            {
                if (IsShowOnHome == null || UrlImageCoverCategory == null)
                    return false;
                return true;
            }).ObservesProperty(() => SelectedParentCategory).ObservesProperty(() => IsShowOnHome);

            UpLoadImageCategoryCommand = new DelegateCommand(() =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        UrlImageCoverCategory = openFileDialog.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageDialogView messageDialogView = new MessageDialogView(ex.Message, 1);
                        messageDialogView.Show();
                    }
                }
            }).ObservesProperty(() => UrlImageCoverCategory);

            UpdateCategoryCommand = new DelegateCommand(async () =>
            {
               if(SelectedCategory != null)
                {
                    CategoryUpdateRequest request = new CategoryUpdateRequest() { ParentId = SelectedParentCategory != null ? SelectedParentCategory.Id : null, IsShowOnHome = IsShowOnHome, Id = SelectedCategory.Id };

                    var result = await _categoryAPI.UpdateCategory(request, SystemConstants.TokenInUse);

                    if (!result.IsSuccessed)
                    {
                        MessageDialogView messageDialogView = new MessageDialogView(result.Message, 1);
                        messageDialogView.Show();
                    }
                    else
                    {
                        SelectedCategory.IsShowOnHome = IsShowOnHome;

                        SetVariableNull();

                        MessageDialogView messageDialogView = new MessageDialogView(result.Message, 0);
                        messageDialogView.Show();
                    }
                }
            }, () =>
            {
                if (SelectedCategory == null)
                    return false;
                return true;
            }).ObservesProperty(() => SelectedCategory);

            DeleteCategoryCommand = new DelegateCommand(async () =>
            {
                if(SelectedCategory != null)
                {
                    var result = await _categoryAPI.DeleteCategory(SelectedCategory.Id, SystemConstants.TokenInUse);

                    if (!result.IsSuccessed)
                    {
                        MessageDialogView messageDialogView = new MessageDialogView(result.Message, 1);
                        messageDialogView.Show();
                    }
                    else
                    {
                        SelectedCategory.IsActive = !SelectedCategory.IsActive;

                        SetVariableNull();

                        MessageDialogView messageDialogView = new MessageDialogView(result.Message, 0);
                        messageDialogView.Show();
                    }
                }
            }, () =>
            {
                if (SelectedCategory == null)
                    return false;
                return true;
            }).ObservesProperty(() => SelectedCategory);
            #endregion

            #region category translation command
            AddCategoryTranslationViewCommand = new DelegateCommand(async () =>
            {
                if(SelectedCategory != null)
                {
                    AddCategoryTranslationView addCategoryTranslationView = new AddCategoryTranslationView();
                    addCategoryTranslationView.DataContext = new AddCategoryTranslationVM(SelectedCategory);
                    addCategoryTranslationView.ShowDialog();

                    CategoryTranslations = await _categoryTranslationAPI.GetByCategoryId(SelectedCategory.Id, SystemConstants.TokenInUse);
                }
                
            }, () =>
            {
                if (SelectedCategory == null)
                    return false;
                return true;
            }).ObservesProperty(() => SelectedCategory);

            UpdateCategoryTranslationViewCommand = new DelegateCommand(async () =>
            {
                if (SelectedCategoryTranslation != null && SelectedCategory != null)
                {
                    UpdateCategoryTranslationView updateCategoryTranslationView = new UpdateCategoryTranslationView();
                    updateCategoryTranslationView.DataContext = new UpdateCategoryTranslationVM(SelectedCategoryTranslation);
                    updateCategoryTranslationView.ShowDialog();

                    CategoryTranslations = await _categoryTranslationAPI.GetByCategoryId(SelectedCategory.Id, SystemConstants.TokenInUse);
                }
            }, () =>
            {
                if (SelectedCategory == null || SelectedCategoryTranslation == null)
                    return false;
                return true;
            }).ObservesProperty(() => SelectedCategory).ObservesProperty(() => SelectedCategoryTranslation);

            DeleteCategoryTranslationCommand = new DelegateCommand(async () =>
            {
                if(SelectedCategoryTranslation != null && SelectedCategory != null)
                {
                    var result = await _categoryTranslationAPI.DeleteCategoryTranslation(SelectedCategoryTranslation.Id, SystemConstants.TokenInUse);

                    if (!result.IsSuccessed)
                    {
                        MessageDialogView messageDialogView = new MessageDialogView(result.Message, 1);
                        messageDialogView.Show();
                    }
                    else
                    {
                        CategoryTranslations = await _categoryTranslationAPI.GetByCategoryId(SelectedCategory.Id, SystemConstants.TokenInUse);

                        MessageDialogView messageDialogView = new MessageDialogView(result.Message, 0);
                        messageDialogView.Show();
                    }
                }
            }, () =>
            {
                if (SelectedCategory == null || SelectedCategoryTranslation == null)
                    return false;
                return true;
            }).ObservesProperty(() => SelectedCategory).ObservesProperty(() => SelectedCategoryTranslation);
            #endregion
        }

        private void SetVariableNull()
        {
            SelectedCategory = null;
            IsShowOnHome = null;
            SelectedParentCategory = null;
            UrlImageCoverCategory = "";
        }

        private async void Loaded()
        {
            Categories = new ObservableCollection<Category>();
            CategoryTranslations = new ObservableCollection<CategoryTranslation>();
            ParentIdFilterByCategoryTranslations = new ObservableCollection<CategoryTranslation>();

            CategoryIdChoosed = "Category Translation";

            ParentIdFilterByCategoryTranslations = await _categoryTranslationAPI.GetAllCategoryTranslationByLanguageId(SystemConstants.LanguageIdInUse, SystemConstants.TokenInUse);

            PagedResult<Category> pageCategory = await _categoryAPI.GetAllCategories(1, 20, SystemConstants.TokenInUse);

            Categories = pageCategory.Items;

        }
    }
}
