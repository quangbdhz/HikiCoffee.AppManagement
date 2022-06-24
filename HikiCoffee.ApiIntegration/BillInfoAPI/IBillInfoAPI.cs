using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Models.DataRequest.BillInfos;
using System.Collections.ObjectModel;

namespace HikiCoffee.ApiIntegration.BillInfoAPI
{
    public interface IBillInfoAPI
    {
        Task<ApiResult<int>> AddBillInfo(BillInfoCreateRequest request, string? token);

        Task<ApiResult<bool>> UpdateBillInfo(BillInfoUpdateRequest request, string? token);

        Task<ApiResult<bool>> DeleteBillInfo(int billId, int productId, string? token);

        Task<ObservableCollection<BillInfo>> GetAllBillInfoCustomerOrder(int billId, int languageId, string? token);

        Task<ObservableCollection<BillInfo>> GetBillInfoByBillId(int billId, int languageId, string? token);
    }
}
