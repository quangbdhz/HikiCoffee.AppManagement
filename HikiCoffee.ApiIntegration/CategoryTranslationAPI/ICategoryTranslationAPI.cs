using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Models.DataRequest.CategoryTranslations;
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

        Task<ObservableCollection<CategoryTranslation>> GetByCategoryId(int categoryId, string? token);

        Task<ApiResult<int>> AddCategoryTranslation(CategoryTranslationCreateRequest request, string? token);

        Task<ApiResult<bool>> UpdateCategoryTranslation(CategoryTranslationUpdateRequest request, string? token);

        Task<ApiResult<bool>> DeleteCategoryTranslation(int categoryTranslationId, string? token);
    }
}
