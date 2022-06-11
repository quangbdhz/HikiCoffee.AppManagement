using HikiCoffee.Models;
using HikiCoffee.Models.Common;

namespace HikiCoffee.ApiIntegration.CategoryAPI
{
    public interface ICategoryAPI
    {
        Task<PagedResult<Category>> GetAllCategories(int pageIndex, int pageSize, string token);
    }
}
