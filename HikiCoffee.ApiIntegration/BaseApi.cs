using HikiCoffee.Models.Common;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Text;

namespace HikiCoffee.ApiIntegration
{
    public class BaseAPI
    {

        public async Task<T> GetAsync<T>(string url, string? token)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.BaseAddress = new Uri(url);

            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                T myDeserializedObjList = (T)JsonConvert.DeserializeObject(body, typeof(T));

                if (myDeserializedObjList != null)
                    return myDeserializedObjList;
            }
            else
            {
                T myDeserializedObjList = (T)JsonConvert.DeserializeObject(body, typeof(T));

                if (myDeserializedObjList != null)
                    return myDeserializedObjList;
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<ObservableCollection<T>> GetListAsync<T>(string url, string? token)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.BaseAddress = new Uri(url);

            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var data = (ObservableCollection<T>)JsonConvert.DeserializeObject(body, typeof(ObservableCollection<T>));

                if (data != null)
                    return data;
            }

            throw new Exception(body);
        }

        public async Task<ApiResult<Guid>> PostAsync<T>(string url, T obj)
        {
            try
            {
                var json = JsonConvert.SerializeObject(obj);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var client = new HttpClient();

                var response = await client.PostAsync(url, data);

                var body = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = (ApiResult<Guid>)JsonConvert.DeserializeObject(body, typeof(ApiResult<Guid>));

                    if (result != null)
                        return result;
                }
                else
                {
                    var result = (ApiResult<Guid>)JsonConvert.DeserializeObject(body, typeof(ApiResult<Guid>));

                    if (result == null)
                        return new ApiErrorResult<Guid>();
                    return result;
                }

                throw new Exception(body);
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<Guid>(ex.Message);
            }


        }

        public async Task<string> DeleteAsync(string url, string? token)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri(url);

                var response = await client.DeleteAsync(url);

                var body = await response.Content.ReadAsStringAsync();

                return body;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> UpdateAsync<T>(string url, string? token, T obj)
        {
            try
            {
                var json = JsonConvert.SerializeObject(obj);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri(url);

                var response = await client.PutAsync(url, data);

                var body = await response.Content.ReadAsStringAsync();

                return body;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

     
    
        public async Task<ApiResult<int>> ApiResultPostAsync<T>(string url, string? token, T request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri(url);

                var response = await client.PostAsync(url, data);
                var body = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = (ApiResult<int>)JsonConvert.DeserializeObject(body, typeof(ApiResult<int>));

                    if (result != null)
                        return result;
                }
                else
                {
                    var result = (ApiResult<int>)JsonConvert.DeserializeObject(body, typeof(ApiResult<int>));

                    if (result != null)
                        return result;
                }

                throw new Exception(body);
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<int>(ex.Message);
            }
        }

        public async Task<ApiResult<bool>> ApiResultPutAsync<T>(string url, string? token, T request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri(url);

                var response = await client.PutAsync(url, data);
                var body = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = (ApiResult<bool>)JsonConvert.DeserializeObject(body, typeof(ApiResult<bool>));

                    if (result != null)
                        return result;
                }
                else
                {
                    var result = (ApiResult<bool>)JsonConvert.DeserializeObject(body, typeof(ApiResult<bool>));

                    if (result != null)
                        return result;
                }

                throw new Exception(body);
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>(ex.Message);
            }
        }
        
        public async Task<ApiResult<bool>> ApiResultDeleteAsync(string url, string? token)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri(url);

                var response = await client.DeleteAsync(url);

                var body = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = (ApiResult<bool>)JsonConvert.DeserializeObject(body, typeof(ApiResult<bool>));

                    if (result != null)
                        return result;
                }
                else
                {
                    var result = (ApiResult<bool>)JsonConvert.DeserializeObject(body, typeof(ApiResult<bool>));

                    if (result != null)
                        return result;
                }

                throw new Exception(body);
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<bool>(ex.Message);
            }
        }

        public async Task<ApiResult<int>> ApiResultGetAsync(string url, string? token)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri(url);

                var response = await client.GetAsync(url);

                var body = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = (ApiResult<int>)JsonConvert.DeserializeObject(body, typeof(ApiResult<int>));

                    if (result != null)
                        return result;
                }
                else
                {
                    var result = (ApiResult<int>)JsonConvert.DeserializeObject(body, typeof(ApiResult<int>));

                    if (result != null)
                        return result;
                }

                throw new Exception(body);
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<int>(ex.Message);
            }
        }
    }
}
