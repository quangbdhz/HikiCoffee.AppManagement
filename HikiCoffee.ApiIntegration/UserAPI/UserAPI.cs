using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Utilities;

namespace HikiCoffee.ApiIntegration.UserAPI
{
    public class UserAPI : BaseAPI, IUserAPI
    {
        public async Task<string> DeleteUser(Guid userId, string? token)
        {
            return await DeleteAsync(SystemConstants.DomainName + $"/api/Users/Delete/{userId.ToString()}", token);
        }

        public async Task<PagedResult<User>> GetAllUsers(int pageIndex, int pageSize, string? token)
        {
            return await GetAsync<PagedResult<User>>(SystemConstants.DomainName + $"/api/Users/GetPagingUserManagements?PageIndex={pageIndex}&PageSize={pageSize}", token);
        }
    }
}
