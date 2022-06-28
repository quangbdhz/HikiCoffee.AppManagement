using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Models.DataRequest.ProductTranslations;
using HikiCoffee.Utilities;
using System.Collections.ObjectModel;

namespace HikiCoffee.ApiIntegration.ProductTranslationAPI
{
    public class ProductTranslationAPI : BaseAPI, IProductTranslationAPI
    {
        public async Task<ApiResult<int>> AddProductTranslation( string? token, ProductTranslationCreateRequest request)
        {
            return await ApiResultPostAsync<ProductTranslationCreateRequest>(SystemConstants.DomainName + $"/api/ProductTranslations/Add", token, request);
        }

        public async Task<ApiResult<bool>> DeleteProductTranslation(int productTransltionId, string? token)
        {
            return await ApiResultDeleteAsync(SystemConstants.DomainName + $"/api/ProductTranslations/Delete/{productTransltionId}", token);
        }

        public async Task<ObservableCollection<ItemOrder>> GetAllProductTranslationByCategoryId(int categoryId, int languageId, string token)
        {
            return await GetListAsync<ItemOrder>(SystemConstants.DomainName + $"/api/ProductTranslations/GetAllByCategoryId/{categoryId}/{languageId}", token);
        }

        public async Task<ObservableCollection<ProductTranslation>> GetAllProductTranslationByLanguageId(int languageId, string token)
        {
            return await GetListAsync<ProductTranslation>(SystemConstants.DomainName + $"/api/ProductTranslations/GetAllByLanguageId/{languageId}", token);
        }

        public async Task<ObservableCollection<ProductTranslation>> GetAllProductTranslations(int productId, string token)
        {
            return await GetListAsync<ProductTranslation>(SystemConstants.DomainName + $"/api/ProductTranslations/GetByProductId/{productId}", token);
        }

        public async Task<ApiResult<bool>> UpdateProductTranslation(string? token, ProductTranslationUpdateRequest request)
        {
            return await ApiResultPutAsync<ProductTranslationUpdateRequest>(SystemConstants.DomainName + $"/api/ProductTranslations/Update", token, request);
        }
    }
}
