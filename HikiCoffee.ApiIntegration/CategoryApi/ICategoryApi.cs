using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Models.DataRequest.Categories;

namespace HikiCoffee.ApiIntegration.CategoryAPI
{
    public interface ICategoryAPI
    {
        Task<PagedResult<Category>> GetAllCategories(int pageIndex, int pageSize, string token);

        Task<ApiResult<int>> AddCategory(CategoryCreateRequest request, string? token);

        Task<ApiResult<bool>> UpdateCategory(CategoryUpdateRequest request, string? token);

        Task<ApiResult<bool>> DeleteCategory(int categoryId, string? token);
    }
}
