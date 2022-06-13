using HikiCoffee.Models;
using HikiCoffee.Models.DataRequest.Supliers;
using System.Collections.ObjectModel;

namespace HikiCoffee.ApiIntegration.SuplierAPI
{
    public interface ISuplierAPI
    {
        Task<ObservableCollection<Suplier>> GetAllSupliers(string? token);

        Task<string> AddSuplier(SuplierCreateRequest request, string? token);

        Task<string> UpdateSuplier(SuplierUpdateRequest request, string? token);

        Task<string> DeleteSuplier(int suplierId, string? token);
    }
}
