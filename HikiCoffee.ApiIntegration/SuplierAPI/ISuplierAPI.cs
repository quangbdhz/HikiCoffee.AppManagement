using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Models.DataRequest.Supliers;
using System.Collections.ObjectModel;

namespace HikiCoffee.ApiIntegration.SuplierAPI
{
    public interface ISuplierAPI
    {
        Task<ObservableCollection<Suplier>> GetAllSupliers(string? token);

        Task<ApiResult<int>> AddSuplier(SuplierCreateRequest request, string? token);

        Task<ApiResult<bool>> UpdateSuplier(SuplierUpdateRequest request, string? token);

        Task<ApiResult<bool>> DeleteSuplier(int suplierId, string? token);
    }
}
