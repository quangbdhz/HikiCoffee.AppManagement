using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Utilities;

namespace HikiCoffee.ApiIntegration.CategoryApi
{
    public class CategoryApi : BaseApi, ICategoryApi
    {
        public async Task<PagedResult<Category>> GetAll(int pageIndex, int pageSize)
        {
            return await GetAsync<PagedResult<Category>>(SystemConstants.DomainName + $"/api/Categories/GetPagingCategoryManagements?PageIndex={pageIndex}&PageSize={pageSize}");
        }
    }
}
