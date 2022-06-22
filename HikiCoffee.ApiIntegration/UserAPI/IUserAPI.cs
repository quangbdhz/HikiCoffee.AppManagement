using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Models.DataRequest.Users;

namespace HikiCoffee.ApiIntegration.UserAPI
{
    public interface IUserAPI
    {
        Task<PagedResult<User>> GetAllUsers(int pageIndex, int pageSize, string? token);

        Task<ApiResult<Guid>> Login(LoginRequest request);

        Task<User> GetByUserLoginAppManagement(Guid userId, string? token);

        Task<string> DeleteUser(Guid userId, string? token);
    }
}
