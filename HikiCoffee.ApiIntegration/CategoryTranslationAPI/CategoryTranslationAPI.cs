using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Models.DataRequest.CategoryTranslations;
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

        public async Task<ObservableCollection<CategoryTranslation>> GetByCategoryId(int categoryId, string? token)
        {
            return await GetListAsync<CategoryTranslation>(SystemConstants.DomainName + $"/api/CategoryTranslations/GetByCategoryId/{categoryId}", token);
        }

        public async Task<ApiResult<int>> AddCategoryTranslation(CategoryTranslationCreateRequest request, string? token)
        {
            return await ApiResultPostAsync<CategoryTranslationCreateRequest>(SystemConstants.DomainName + $"/api/CategoryTranslations/Add", token, request);
        }

        public async Task<ApiResult<bool>> UpdateCategoryTranslation(CategoryTranslationUpdateRequest request, string? token)
        {
            return await ApiResultPutAsync<CategoryTranslationUpdateRequest>(SystemConstants.DomainName + $"/api/CategoryTranslations/Update", token, request);
        }

        public async Task<ApiResult<bool>> DeleteCategoryTranslation(int categoryTranslationId, string? token)
        {
            return await ApiResultDeleteAsync(SystemConstants.DomainName + $"/api/CategoryTranslations/Delete/{categoryTranslationId}", token);
        }
    }
}
