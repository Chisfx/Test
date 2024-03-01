using Newtonsoft.Json;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Json;
namespace Test.Wpf.Helpers
{
    public static class ClientHttp
    {
        private static HttpClient _httpClient = new HttpClient();
        public static List<T>? GetAll<T>(string rutaapi)
        {
            List<T>? result = new List<T>();

            if (_httpClient.BaseAddress == null) _httpClient.BaseAddress = new Uri(Router.UrlBase);
            result = _httpClient.GetFromJsonAsync<List<T>>(rutaapi).Result;

            return result;
        }

        public static T? Get<T>(string rutaapi)
        {
            T? result = default;

            if (_httpClient.BaseAddress == null) _httpClient.BaseAddress = new Uri(Router.UrlBase);
            result = _httpClient.GetFromJsonAsync<T>(rutaapi).Result;

            return result;
        }

        public static void Post<T>(string rutaapi, T obj)
        {
            if (_httpClient.BaseAddress == null) _httpClient.BaseAddress = new Uri(Router.UrlBase);
            var response = _httpClient.PostAsJsonAsync<T>(rutaapi, obj).Result;

            if (!response.IsSuccessStatusCode)
            {
                var errorJson = response.Content.ReadAsStringAsync().Result;
                dynamic errorObject = JsonConvert.DeserializeObject<ExpandoObject>(errorJson)!;
                throw new Exception(errorObject.Message);
            }
        }

        public static void Put<T>(string rutaapi, T obj)
        {
            if (_httpClient.BaseAddress == null) _httpClient.BaseAddress = new Uri(Router.UrlBase);
            var response = _httpClient.PutAsJsonAsync<T>(rutaapi, obj).Result;

            if (!response.IsSuccessStatusCode)
            {
                var errorJson = response.Content.ReadAsStringAsync().Result;
                dynamic errorObject = JsonConvert.DeserializeObject<ExpandoObject>(errorJson)!;
                throw new Exception(errorObject.Message);
            }
        }

        public static void Delete(string rutaapi)
        {
            if (_httpClient.BaseAddress == null) _httpClient.BaseAddress = new Uri(Router.UrlBase);
            var response = _httpClient.DeleteAsync(rutaapi).Result;

            if (!response.IsSuccessStatusCode)
            {
                var errorJson = response.Content.ReadAsStringAsync().Result;
                dynamic errorObject = JsonConvert.DeserializeObject<ExpandoObject>(errorJson)!;
                throw new Exception(errorObject.Message);
            }
        }
    }
}
