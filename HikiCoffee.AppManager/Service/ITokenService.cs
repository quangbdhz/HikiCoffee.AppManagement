using System.Threading.Tasks;

namespace HikiCoffee.AppManager.Service
{
    public interface ITokenService
    {
        bool SaveToken(string token);

        string ReadToken();
    }
}
