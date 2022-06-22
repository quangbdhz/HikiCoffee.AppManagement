using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using System.Collections.ObjectModel;

namespace HikiCoffee.ApiIntegration.CategoryTranslationAPI
{
    public interface ICategoryTranslationAPI
    {
        Task<ObservableCollection<CategoryTranslation>> GetAllCategoryTranslationByLanguageId(int languageId, string? token);

        Task<ObservableCollection<CategoryTranslation>> GetAllCategoryTranslationOfProduct(int languageId, int productId, string? token);

        Task<ObservableCollection<CategoryTranslationWithUrl>> GetAllCategoryTranslationWithUrlByLanguageId(int languageId, string? token);

        Task<ApiResult<bool>> DeleteCategoryInProduct(int productId, int categoryId, string? token);

        Task<ApiResult<int>> AddCategoryInProduct(ProductInCategory productInCategory, string? token);
    }
}
