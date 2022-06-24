using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Models.DataRequest.Categories;
using HikiCoffee.Utilities;

namespace HikiCoffee.ApiIntegration.CategoryAPI
{
    public class CategoryAPI : BaseAPI, ICategoryAPI
    {
        public async Task<ApiResult<int>> AddCategory(CategoryCreateRequest request, string? token)
        {
            return await ApiResultPostAsync<CategoryCreateRequest>(SystemConstants.DomainName + $"/api/Categories/Add", token, request);
        }

        public async Task<ApiResult<bool>> DeleteCategory(int categoryId, string? token)
        {
            return await ApiResultDeleteAsync(SystemConstants.DomainName + $"/api/Categories/Delete/{categoryId}", token);
        }

        public async Task<PagedResult<Category>> GetAllCategories(int pageIndex, int pageSize, string token)
        {
            return await GetAsync<PagedResult<Category>>(SystemConstants.DomainName + $"/api/Categories/GetPagingCategoryManagements?PageIndex={pageIndex}&PageSize={pageSize}", token);
        }

        public async Task<ApiResult<bool>> UpdateCategory(CategoryUpdateRequest request, string? token)
        {
            return await ApiResultPutAsync<CategoryUpdateRequest>(SystemConstants.DomainName + $"/api/Categories/Update", token, request);
        }
    }
}
