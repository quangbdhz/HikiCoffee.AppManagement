using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Utilities;
using System.Collections.ObjectModel;

namespace HikiCoffee.ApiIntegration.CategoryTranslationAPI
{
    public class CategoryTranslationAPI : BaseAPI, ICategoryTranslationAPI
    {
        public async Task<ApiResult<bool>> DeleteCategoryInProduct(int productId, int categoryId, string? token)
        {
            return await ApiResultDeleteAsync(SystemConstants.DomainName + $"/api/ProductInCategories/Delete/{productId}/{categoryId}", token);
        }

        public async Task<ObservableCollection<CategoryTranslation>> GetAllCategoryTranslationByLanguageId(int languageId, string? token)
        {
            return await GetListAsync<CategoryTranslation>(SystemConstants.DomainName + $"/api/CategoryTranslations/GetAllCategoryTranslationByLanguageId/{languageId}", token);
        }

        public async Task<ObservableCollection<CategoryTranslation>> GetAllCategoryTranslationOfProduct(int languageId, int productId, string? token)
        {
            return await GetListAsync<CategoryTranslation>(SystemConstants.DomainName + $"/api/ProductInCategories/GetCategoryOfProduct/{languageId}/{productId}", token);
        }

        public async Task<ApiResult<int>> AddCategoryInProduct(ProductInCategory productInCategory, string? token)
        {
            return await ApiResultPostAsync<ProductInCategory>(SystemConstants.DomainName + $"/api/ProductInCategories/Add", token, productInCategory);
        }

        public async Task<ObservableCollection<CategoryTranslationWithUrl>> GetAllCategoryTranslationWithUrlByLanguageId(int languageId, string? token)
        {
            return await GetListAsync<CategoryTranslationWithUrl>(SystemConstants.DomainName + $"/api/CategoryTranslations/GetAllCategoryTranslationWithUrlByLanguageId/{languageId}", token);
        }
    }
}
