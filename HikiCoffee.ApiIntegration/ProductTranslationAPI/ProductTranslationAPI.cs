using HikiCoffee.Models;
using HikiCoffee.Models.DataRequest.ProductTranslations;
using HikiCoffee.Utilities;
using System.Collections.ObjectModel;

namespace HikiCoffee.ApiIntegration.ProductTranslationAPI
{
    public class ProductTranslationAPI : BaseAPI, IProductTranslationAPI
    {
        public async Task<string> AddProductTranslation( string? token, ProductTranslationCreateRequest request)
        {
            return await PostAsync<ProductTranslationCreateRequest>(SystemConstants.DomainName + $"/api/ProductTranslations/Add", token, request);
        }

        public async Task<string> DeleteProductTranslation(int productTransltionId, string? token)
        {
            return await DeleteAsync(SystemConstants.DomainName + $"/api/ProductTranslations/Delete/{productTransltionId}", token);
        }

        public async Task<ObservableCollection<ProductTranslation>> GetAllProductTranslationByLanguageId(int languageId, string token)
        {
            return await GetListAsync<ProductTranslation>(SystemConstants.DomainName + $"/api/ProductTranslations/GetAllByLanguageId/{languageId}", token);
        }

        public async Task<ObservableCollection<ProductTranslation>> GetAllProductTranslations(int productId, string token)
        {
            return await GetListAsync<ProductTranslation>(SystemConstants.DomainName + $"/api/ProductTranslations/GetByProductId/{productId}", token);
        }

        public async Task<string> UpdateProductTranslation(string? token, ProductTranslationUpdateRequest request)
        {
            return await PostAsync<ProductTranslationUpdateRequest>(SystemConstants.DomainName + $"/api/ProductTranslations/Update", token, request);
        }
    }
}
