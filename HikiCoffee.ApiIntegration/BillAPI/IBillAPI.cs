using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Models.DataRequest.Bills;

namespace HikiCoffee.ApiIntegration.BillAPI
{
    public interface IBillAPI
    {
        Task<ApiResult<int>> AddBill(BillCreateRequest request, string? token);

        Task<ApiResult<bool>> MergeBill(MergeBillRequest request, string? token);

        Task<ApiResult<bool>> BillCheckOut(BillCheckOutRequest request, string? token);

        Task<Bill> GetBillIdOfCoffeeTable(int coffeeTableId, string? token);
    }
}
