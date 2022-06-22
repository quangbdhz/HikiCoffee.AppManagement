using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Models.DataRequest.CoffeeTables;
using System.Collections.ObjectModel;

namespace HikiCoffee.ApiIntegration.CoffeeTableAPI
{
    public interface ICoffeeTableAPI
    {
        Task<ObservableCollection<CoffeeTable>> GetAllCoffeeTable(string? token);

        Task<ApiResult<bool>> ChangeStatusCoffeeTable(ChangeStatusCoffeeTableRequest request, string? token);

        Task<ApiResult<bool>> ChangeCoffeeTableIdInBill(ChangeCoffeeTableIdInBillRequest request, string? token);
    }
}
