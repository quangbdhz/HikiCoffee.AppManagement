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
    }
}
