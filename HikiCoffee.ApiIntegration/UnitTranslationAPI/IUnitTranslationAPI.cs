using HikiCoffee.Models;
using System.Collections.ObjectModel;

namespace HikiCoffee.ApiIntegration.UnitTranslationAPI
{
    public interface IUnitTranslationAPI
    {
        Task<ObservableCollection<UnitTranslation>> GetAllUnitTranslation(int languageId, string? token);
    }
}
