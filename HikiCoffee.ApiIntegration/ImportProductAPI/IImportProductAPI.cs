using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Models.DataRequest.ImportProducts;

namespace HikiCoffee.ApiIntegration.ImportProductAPI
{
    public interface IImportProductAPI
    {
        Task<PagedResult<ImportProduct>> GetAllImportProduct(int pageIndex, int pageSize, int languageId, string? token);

        Task<ApiResult<int>> AddImportProduct(ImportProductCreateRequest request, string? token);

    }
}
