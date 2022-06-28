using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Models.DataRequest.Supliers;
using HikiCoffee.Utilities;
using System.Collections.ObjectModel;

namespace HikiCoffee.ApiIntegration.SuplierAPI
{
    public class SuplierAPI : BaseAPI, ISuplierAPI
    {
        public async Task<ApiResult<int>> AddSuplier(SuplierCreateRequest request, string? token)
        {
            return await ApiResultPostAsync<SuplierCreateRequest>(SystemConstants.DomainName + $"/api/Supliers/Add", token, request);
        }

        public async Task<ApiResult<bool>> DeleteSuplier(int suplierId, string? token)
        {
            return await ApiResultDeleteAsync(SystemConstants.DomainName + $"/api/Supliers/Delete/{suplierId}", token);
        }

        public async Task<ObservableCollection<Suplier>> GetAllSupliers(string? token)
        {
            return await GetListAsync<Suplier>(SystemConstants.DomainName + $"/api/Supliers/GetAll", token);
        }

        public async Task<ObservableCollection<Suplier>> GetAllSuplierManagements(string? token)
        {
            return await GetListAsync<Suplier>(SystemConstants.DomainName + $"/api/Supliers/GetAllSuplierManagements", token);
        }

        public async Task<ApiResult<bool>> UpdateSuplier(SuplierUpdateRequest request, string? token)
        {
            return await ApiResultPutAsync<SuplierUpdateRequest>(SystemConstants.DomainName + $"/api/Supliers/Update", token, request);
        }
    }
}
