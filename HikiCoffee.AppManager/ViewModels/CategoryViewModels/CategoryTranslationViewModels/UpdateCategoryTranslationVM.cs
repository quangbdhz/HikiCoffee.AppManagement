using HikiCoffee.ApiIntegration.CategoryTranslationAPI;
using HikiCoffee.AppManager.Views.MessageDialogViews;
using HikiCoffee.Models;
using HikiCoffee.Models.DataRequest.CategoryTranslations;
using HikiCoffee.Utilities;
using Prism.Commands;
using Prism.Mvvm;

namespace HikiCoffee.AppManager.ViewModels.CategoryViewModels.CategoryTranslationViewModels
{
    public class UpdateCategoryTranslationVM : BindableBase
    {
        private string _nameCategory;
        public string NameCategory
        {
            get { return _nameCategory; }
            set { SetProperty(ref _nameCategory, value); }
        }

        private string _seoDescription;
        public string SeoDescription
        {
            get { return _seoDescription; }
            set { SetProperty(ref _seoDescription, value); }
        }

        private string _seoTitle;
        public string SeoTitle
        {
            get { return _seoTitle; }
            set { SetProperty(ref _seoTitle, value); }
        }

        public DelegateCommand UpdateCategoryTranslationCommand { get; set; }

        private readonly ICategoryTranslationAPI _categoryTranslationAPI;

        public UpdateCategoryTranslationVM(CategoryTranslation categoryTranslation)
        {
            _categoryTranslationAPI = new CategoryTranslationAPI();

            NameCategory = categoryTranslation.NameCategory;
            SeoDescription = categoryTranslation.SeoDescription;
            SeoTitle = categoryTranslation.SeoTitle;

            UpdateCategoryTranslationCommand = new DelegateCommand(async () => 
            { 
                if(categoryTranslation != null)
                {
                    CategoryTranslationUpdateRequest request = new CategoryTranslationUpdateRequest() { Id = categoryTranslation.Id, NameCategory = NameCategory, SeoDescription = SeoDescription, SeoTitle = SeoTitle  };

                    var result = await _categoryTranslationAPI.UpdateCategoryTranslation(request, SystemConstants.TokenInUse);

                    if (!result.IsSuccessed)
                    {
                        MessageDialogView messageDialogView = new MessageDialogView(result.Message, 1);
                        messageDialogView.Show();
                    }
                    else
                    {
                        MessageDialogView messageDialogView = new MessageDialogView(result.Message, 0);
                        messageDialogView.Show();
                    }
                }
            }, () => 
            {
                if (string.IsNullOrEmpty(NameCategory))
                    return false;
                return true;
            }).ObservesProperty(() => NameCategory);
        }
    }
}
