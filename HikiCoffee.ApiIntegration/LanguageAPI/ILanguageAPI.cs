using HikiCoffee.Models;
using System.Collections.ObjectModel;

namespace HikiCoffee.ApiIntegration.LanguageAPI
{
    public interface ILanguageAPI
    {
        Task<ObservableCollection<Language>> GetAllLanguages(string? token);
    }
}
