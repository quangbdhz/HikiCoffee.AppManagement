using HikiCoffee.ApiIntegration.CategoryTranslationAPI;
using HikiCoffee.ApiIntegration.LanguageAPI;
using HikiCoffee.AppManager.Views.MessageDialogViews;
using HikiCoffee.Models;
using HikiCoffee.Models.DataRequest.CategoryTranslations;
using HikiCoffee.Utilities;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace HikiCoffee.AppManager.ViewModels.CategoryViewModels.CategoryTranslationViewModels
{
    public class AddCategoryTranslationVM : BindableBase
    {
        private ObservableCollection<Language> _languages;
        public ObservableCollection<Language> Languages
        {
            get { return _languages; }
            set { SetProperty(ref _languages, value); }
        }

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

        private int _languageId;
        public int LanguageId
        {
            get { return _languageId; }
            set { SetProperty(ref _languageId, value); }
        }

        public DelegateCommand<Language> SelectLanguageCommand { get; set; }

        public DelegateCommand AddCategoryTranslationCommand { get; set; }
        
        private readonly ILanguageAPI _languageAPI;

        private readonly ICategoryTranslationAPI _categoryTranslationAPI;

        public AddCategoryTranslationVM(Category category)
        {
            _languageAPI = new LanguageAPI();
            _categoryTranslationAPI = new CategoryTranslationAPI();

            Loaded();

            SelectLanguageCommand = new DelegateCommand<Language>((p) =>
            {
                if (p != null)
                    LanguageId = p.Id;
            });

            AddCategoryTranslationCommand = new DelegateCommand(async () => 
            {
                CategoryTranslationCreateRequest request = new CategoryTranslationCreateRequest() 
                { 
                    CategoryId = category.Id, 
                    LanguageId = LanguageId, 
                    NameCategory = NameCategory, 
                    SeoDescription = SeoDescription, 
                    SeoTitle = SeoTitle 
                };

                var result = await _categoryTranslationAPI.AddCategoryTranslation(request, SystemConstants.TokenInUse);

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

            }, () => 
            {
                if (LanguageId == 0 || string.IsNullOrEmpty(NameCategory) == true)
                    return false;
                return true;
            }).ObservesProperty(() => LanguageId).ObservesProperty(() => NameCategory);
        }

        public async void Loaded()
        {
            Languages = await _languageAPI.GetAllLanguages(null);
        }
    }
}
