using HikiCoffee.Models;
using HikiCoffee.Utilities;
using System.Collections.ObjectModel;

namespace HikiCoffee.ApiIntegration.UnitTranslationAPI
{
    public class UnitTranslationAPI : BaseAPI, IUnitTranslationAPI
    {
        public async Task<ObservableCollection<UnitTranslation>> GetAllUnitTranslation(int languageId, string? token)
        {
            return await GetListAsync<UnitTranslation>(SystemConstants.DomainName + $"/api/UnitTranslations/GetAllUnitTranslation/{languageId}", token);
        }
    }
}
