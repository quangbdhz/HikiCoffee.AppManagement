using HikiCoffee.Models;
using HikiCoffee.Models.Common;

namespace HikiCoffee.ApiIntegration.CategoryApi
{
    public interface ICategoryApi
    {
        Task<PagedResult<Category>> GetAll(int pageIndex, int pageSize);
    }
}
