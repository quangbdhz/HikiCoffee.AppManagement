using System.Threading.Tasks;

namespace HikiCoffee.AppManager.Service.UploadImageCloudinary
{
    public interface IUploadImageCloudinaryService
    {
        Task<string> UploadImageCloudinary(string filePath);
    }
}
