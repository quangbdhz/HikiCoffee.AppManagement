using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Utilities;

namespace HikiCoffee.ApiIntegration.ProductInCategoryAPI
{
    public class ProductInCategoryAPI : BaseAPI, IProductInCategoryAPI
    {
        public async Task<ApiResult<int>> AddProductInCategory(ProductInCategory request, string? token)
        {
            return await ApiResultPostAsync<ProductInCategory>(SystemConstants.DomainName + "/api/ProductInCategories/Add", token, request);
        }
    }
}
