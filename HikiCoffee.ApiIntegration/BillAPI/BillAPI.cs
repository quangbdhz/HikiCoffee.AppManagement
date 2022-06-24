using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Models.DataRequest.Bills;
using HikiCoffee.Utilities;
using System.Collections.ObjectModel;

namespace HikiCoffee.ApiIntegration.BillAPI
{
    public class BillAPI : BaseAPI, IBillAPI
    {
        public async Task<ApiResult<int>> AddBill(BillCreateRequest request, string? token)
        {
            return await ApiResultPostAsync<BillCreateRequest>(SystemConstants.DomainName + $"/api/Bills/Add", token, request);
        }

        public async Task<ApiResult<bool>> BillCheckOut(BillCheckOutRequest request, string? token)
        {
            return await ApiResultPutAsync<BillCheckOutRequest>(SystemConstants.DomainName + $"/api/Bills/CheckOut", token, request);
        }

        public async Task<PagedResult<Bill>> GetAllBill(int pageIndex, int pageSize, string? token)
        {
            return await GetAsync<PagedResult<Bill>>(SystemConstants.DomainName + $"/api/Bills/GetAllBill?PageIndex={pageIndex}&PageSize={pageSize}", token);
        }

        public async Task<Bill> GetBillIdOfCoffeeTable(int coffeeTableId, string? token)
        {
            return await GetAsync<Bill>(SystemConstants.DomainName + $"/api/Bills/GetBillIdOfCoffeeTable/{coffeeTableId}", token);
        }

        public async Task<ApiResult<bool>> MergeBill(MergeBillRequest request, string? token)
        {
            return await ApiResultPutAsync<MergeBillRequest>(SystemConstants.DomainName + $"/api/Bills/MergeBill", token, request);
        }
    }
}
