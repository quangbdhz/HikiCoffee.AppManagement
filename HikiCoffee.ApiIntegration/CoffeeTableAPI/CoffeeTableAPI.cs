using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Models.DataRequest.CoffeeTables;
using HikiCoffee.Utilities;
using System.Collections.ObjectModel;

namespace HikiCoffee.ApiIntegration.CoffeeTableAPI
{
    public class CoffeeTableAPI : BaseAPI, ICoffeeTableAPI
    {
        public async Task<ApiResult<bool>> ChangeCoffeeTableIdInBill(ChangeCoffeeTableIdInBillRequest request, string? token)
        {
            return await ApiResultPutAsync<ChangeCoffeeTableIdInBillRequest>(SystemConstants.DomainName + $"/api/CoffeeTables/ChangeCoffeeTableIdInBill", token, request);
        }

        public async Task<ApiResult<bool>> ChangeStatusCoffeeTable(ChangeStatusCoffeeTableRequest request, string? token)
        {
            return await ApiResultPutAsync<ChangeStatusCoffeeTableRequest>(SystemConstants.DomainName + $"/api/CoffeeTables/ChangeStatusCoffeeTable", token, request);
        }

        public async Task<ObservableCollection<CoffeeTable>> GetAllCoffeeTable(string? token)
        {
            return await GetListAsync<CoffeeTable>(SystemConstants.DomainName + $"/api/CoffeeTables/GetAllCoffeeTaleManagements", token);
        }
    }
}
