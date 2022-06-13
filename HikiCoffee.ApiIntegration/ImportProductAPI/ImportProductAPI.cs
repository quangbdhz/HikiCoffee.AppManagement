using HikiCoffee.Models;
using HikiCoffee.Models.Common;
using HikiCoffee.Models.DataRequest.ImportProducts;
using HikiCoffee.Utilities;

namespace HikiCoffee.ApiIntegration.ImportProductAPI
{
    public class ImportProductAPI : BaseAPI, IImportProductAPI
    {
        public async Task<string> AddImportProduct(ImportProductCreateRequest request, string? token)
        {
            return await PostAsync<ImportProductCreateRequest>(SystemConstants.DomainName + $"/api/ImportProducts/Add", token, request);
        }

        public async Task<PagedResult<ImportProduct>> GetAllImportProduct(int pageIndex, int pageSize, int languageId, string? token)
        {
            return await GetAsync<PagedResult<ImportProduct>>(SystemConstants.DomainName + $"/api/ImportProducts/GetPagingImportProductManagements?LanguageId={languageId}&PageIndex={pageIndex}&PageSize={pageSize}", token);
        }
    }
}
