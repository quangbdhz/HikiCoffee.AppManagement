using HikiCoffee.Models;
using HikiCoffee.Models.Common;

namespace HikiCoffee.ApiIntegration.ProductInCategoryAPI
{
    public interface IProductInCategoryAPI
    {
        Task<ApiResult<int>> AddProductInCategory(ProductInCategory request, string? token);
    }
}
