using HikiCoffee.Models;
using HikiCoffee.Utilities;
using System.Collections.ObjectModel;

namespace HikiCoffee.ApiIntegration.LanguageAPI
{
    public class LanguageAPI : BaseAPI, ILanguageAPI
    {
        public async Task<ObservableCollection<Language>> GetAllLanguages(string? token)
        {
            return await GetListAsync<Language>(SystemConstants.DomainName + "/api/Languages", token);
        }
    }
}
