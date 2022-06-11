using HikiCoffee.Models;
using System.Collections.ObjectModel;

namespace HikiCoffee.ApiIntegration.CategoryTranslationAPI
{
    public interface ICategoryTranslationAPI
    {
        Task<ObservableCollection<CategoryTranslation>> GetAllCategoryTranslationByLanguageId(int languageId, string? token);

        Task<ObservableCollection<CategoryTranslation>> GetAllCategoryTranslationOfProduct(int languageId, int productId, string? token);

        Task<string> DeleteCategoryInProduct(int productId, int categoryId, string? token);

        Task<string> AddCategoryInProduct(ProductInCategory productInCategory, string? token);
    }
}
