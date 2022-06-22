using HikiCoffee.Models;

namespace HikiCoffee.Utilities
{
    public class SystemConstants
    {
        public static string DomainName { get; set; } = "https://localhost:7227";

        public static async Task<bool> CheckNetwork()
        {
            try
            {
                using(var client = new HttpClient())
                {
                    await client.GetAsync("https://www.google.com/");
                    return true;
                }
            }
            catch 
            {
                return false;
            }
        }

        public static string TokenInUse { get; set; } = string.Empty;

        public static Guid UserIdInUse { get; set; } = Guid.Empty;

        public static User UserLogin { get; set; }

        public static int LanguageIdInUse { get; set; } = 0;

        public static User GetUserOrder { get; set; }

    }
}
