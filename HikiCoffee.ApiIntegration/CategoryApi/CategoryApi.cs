using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Utilities;

namespace HikiCoffee.ApiIntegration.CategoryAPI
{
    public class CategoryAPI : BaseAPI, ICategoryAPI
    {
        public async Task<PagedResult<Category>> GetAllCategories(int pageIndex, int pageSize, string token)
        {
            return await GetAsync<PagedResult<Category>>(SystemConstants.DomainName + $"/api/Categories/GetPagingCategoryManagements?PageIndex={pageIndex}&PageSize={pageSize}", token);
        }
    }
}
