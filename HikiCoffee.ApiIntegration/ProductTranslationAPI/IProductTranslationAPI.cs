using HikiCoffee.Models;
using HikiCoffee.Models.DataRequest.ProductTranslations;
using System.Collections.ObjectModel;

namespace HikiCoffee.ApiIntegration.ProductTranslationAPI
{
    public interface IProductTranslationAPI
    {
        Task<ObservableCollection<ProductTranslation>> GetAllProductTranslations(int productId, string token);

        Task<ObservableCollection<ProductTranslation>> GetAllProductTranslationByLanguageId(int languageId, string token);

        Task<string> AddProductTranslation(string? token, ProductTranslationCreateRequest request);

        Task<string> UpdateProductTranslation(string? token, ProductTranslationUpdateRequest request);

        Task<string> DeleteProductTranslation(int productTransltionId, string? token);
    }
}
