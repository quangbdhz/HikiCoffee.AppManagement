using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Models.DataRequest.Products;

namespace HikiCoffee.ApiIntegration.ProductAPI
{
    public interface IProductAPI
    {
        Task<PagedResult<Product>> GetAllProducts(int pageIndex, int pageSize, string token);

        Task<string> UpdateProduct(ProductUpdateRequest request, string? token);

        Task<string> DeleteProduct(int productId, string? token);
    }
}
