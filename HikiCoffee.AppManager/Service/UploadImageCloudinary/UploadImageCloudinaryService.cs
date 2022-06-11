using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Threading.Tasks;

namespace HikiCoffee.AppManager.Service.UploadImageCloudinary
{
    public class UploadImageCloudinaryService : IUploadImageCloudinaryService
    {
        public static Cloudinary? cloudinary;

        public const string CLOUD_NAME = "https-deptraitd-blogspot-com";
        public const string API_KEY = "935515642992792";
        public const string API_SECRET = "vvVbRs4IqOvQfKNlsPJbyJ3pCCs";

        public async Task<string> UploadImageCloudinary(string filePath)
        {
            try
            {
                Account account = new Account(CLOUD_NAME, API_KEY, API_SECRET);
                cloudinary = new Cloudinary(account);

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(filePath),
                    Folder = "HikiCoffee/Image_Product"
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                return uploadResult.Url.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
