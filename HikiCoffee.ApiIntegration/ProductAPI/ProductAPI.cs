using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Models.DataRequest.Products;
using HikiCoffee.Utilities;

namespace HikiCoffee.ApiIntegration.ProductAPI
{
    public class ProductAPI : BaseAPI, IProductAPI
    {
        public async Task<string> DeleteProduct(int productId, string? token)
        {
            return await DeleteAsync(SystemConstants.DomainName + $"/api/Products/Delete/{productId}", token);
        }

        public async Task<PagedResult<Product>> GetAllProducts(int pageIndex, int pageSize, string token)
        {
            return await GetAsync<PagedResult<Product>>(SystemConstants.DomainName + $"/api/Products/GetPagingProductManagements?PageIndex={pageIndex}&PageSize={pageSize}", token);
        }

        public async Task<string> UpdateProduct(ProductUpdateRequest request, string? token)
        {
            return await UpdateAsync<ProductUpdateRequest>(SystemConstants.DomainName + $"/api/Products/Update", token, request);
        }
    }
}
