using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Models.DataRequest.Products;

namespace HikiCoffee.ApiIntegration.ProductAPI
{
    public interface IProductAPI
    {
        Task<PagedResult<Product>> GetAllProducts(int pageIndex, int pageSize, string token);

        Task<ApiResult<int>> AddProduct(ProductCreateRequest request, string? token);

        Task<ApiResult<bool>> UpdateProduct(ProductUpdateRequest request, string? token);

        Task<ApiResult<bool>> DeleteProduct(int productId, string? token);
    }
}
