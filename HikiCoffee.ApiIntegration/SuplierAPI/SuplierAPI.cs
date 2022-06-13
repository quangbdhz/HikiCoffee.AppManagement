using HikiCoffee.Models;
using HikiCoffee.Models.DataRequest.Supliers;
using HikiCoffee.Utilities;
using System.Collections.ObjectModel;

namespace HikiCoffee.ApiIntegration.SuplierAPI
{
    public class SuplierAPI : BaseAPI, ISuplierAPI
    {
        public async Task<string> AddSuplier(SuplierCreateRequest request, string? token)
        {
            return await PostAsync<SuplierCreateRequest>(SystemConstants.DomainName + $"/api/Supliers/Add", token, request);
        }

        public async Task<string> DeleteSuplier(int suplierId, string? token)
        {
            return await DeleteAsync(SystemConstants.DomainName + $"/api/Supliers/Delete/{suplierId}", token);
        }

        public async Task<ObservableCollection<Suplier>> GetAllSupliers(string? token)
        {
            return await GetListAsync<Suplier>(SystemConstants.DomainName + $"/api/Supliers/GetAll", token);
        }

        public async Task<string> UpdateSuplier(SuplierUpdateRequest request, string? token)
        {
            return await UpdateAsync<SuplierUpdateRequest>(SystemConstants.DomainName + $"/api/Supliers/Update", token, request);
        }
    }
}
