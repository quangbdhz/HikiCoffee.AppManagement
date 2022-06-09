using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HikiCoffee.ApiIntegration
{
    public class BaseApi
    {

        public async Task<T> GetAsync<T>(string url)
        {

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
            client.BaseAddress = new Uri(url);

            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                T myDeserializedObjList = (T)JsonConvert.DeserializeObject(body, typeof(T));

                if (myDeserializedObjList != null)
                    return myDeserializedObjList;
            }

            throw new Exception(body);
        }

        public async Task<List<T>> GetListAsync<T>(string url)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var data = (List<T>)JsonConvert.DeserializeObject(body, typeof(List<T>));

                if (data != null)
                    return data;
            }

            throw new Exception(body);
        }



    }
}
