using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Models.DataRequest.BillInfos;
using HikiCoffee.Utilities;
using System.Collections.ObjectModel;

namespace HikiCoffee.ApiIntegration.BillInfoAPI
{
    public class BillInfoAPI : BaseAPI, IBillInfoAPI
    {
        public async Task<ApiResult<int>> AddBillInfo(BillInfoCreateRequest request, string? token)
        {
            return await ApiResultPostAsync<BillInfoCreateRequest>(SystemConstants.DomainName + $"/api/BillInfos/Add", token, request);
        }

        public async Task<ApiResult<bool>> DeleteBillInfo(int billId, int productId, string? token)
        {
            return await ApiResultDeleteAsync(SystemConstants.DomainName + $"/api/BillInfos/Delete/{billId}/{productId}", token);
        }

        public async Task<ObservableCollection<BillInfo>> GetAllBillInfoCustomerOrder(int billId, int languageId, string? token)
        {
            return await GetListAsync<BillInfo>(SystemConstants.DomainName + $"/api/BillInfos/GetAllBillInfoManagement/{billId}/{languageId}", token);
        }

        public async Task<ApiResult<bool>> UpdateBillInfo(BillInfoUpdateRequest request, string? token)
        {
            return await ApiResultPutAsync<BillInfoUpdateRequest>(SystemConstants.DomainName + $"/api/BillInfos/Update", token, request);
        }
    }
}
