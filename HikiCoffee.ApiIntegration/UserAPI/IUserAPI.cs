using HikiCoffee.Models;
using HikiCoffee.Models.Common;

namespace HikiCoffee.ApiIntegration.UserAPI
{
    public interface IUserAPI
    {
        Task<PagedResult<User>> GetAllUsers(int pageIndex, int pageSize, string? token);

        Task<string> DeleteUser(Guid userId, string? token);
    }
}
