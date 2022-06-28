using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Models.DataRequest.ProductTranslations;
using System.Collections.ObjectModel;

namespace HikiCoffee.ApiIntegration.ProductTranslationAPI
{
    public interface IProductTranslationAPI
    {
        Task<ObservableCollection<ProductTranslation>> GetAllProductTranslations(int productId, string token);

        Task<ObservableCollection<ProductTranslation>> GetAllProductTranslationByLanguageId(int languageId, string token);
        
        Task<ObservableCollection<ItemOrder>> GetAllProductTranslationByCategoryId(int categoryId, int languageId, string token);

        Task<ApiResult<int>> AddProductTranslation(string? token, ProductTranslationCreateRequest request);

        Task<ApiResult<bool>> UpdateProductTranslation(string? token, ProductTranslationUpdateRequest request);

        Task<ApiResult<bool>> DeleteProductTranslation(int productTransltionId, string? token);
    }
}
