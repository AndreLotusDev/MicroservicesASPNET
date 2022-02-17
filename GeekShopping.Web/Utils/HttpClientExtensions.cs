using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GeekShopping.Web.Utils
{
    public static class HttpClientExtensions
    {
        private static MediaTypeHeaderValue contentType = new MediaTypeHeaderValue("application/json");
        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            var wentWrong = !response.IsSuccessStatusCode;
            if (wentWrong)
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

            var dataAsString = await response.Content.ReadAsStringAsync();
            byte[] dataAsBytes = Encoding.ASCII.GetBytes(dataAsString);
            var streamOfData = new MemoryStream(dataAsBytes);

            return await JsonSerializer.DeserializeAsync<T>(streamOfData, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true});
        }

        public static async Task<HttpResponseMessage> PutAsJsonAsync<T> (this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString);

            content.Headers.ContentType = contentType;

            return await httpClient.PutAsync(url, content);
        }

        public static async Task<HttpResponseMessage> PostAsJsonAsync<T> (this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString);

            content.Headers.ContentType = contentType;
            
            return await httpClient.PostAsync(url, content);
        }

        public static async Task<HttpResponseMessage> PuttAsJsonAsync<T>(this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString);

            content.Headers.ContentType = contentType;

            return await httpClient.PutAsync(url, content);
        }
    }
}
