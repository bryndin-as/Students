using Newtonsoft.Json;
using System.Text;

namespace StudentService.Client.Core.Utils
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _client;
        private readonly string _host;

        public ApiClient(HttpClient client, string host)
        {
            _client = client;
            _host = host;
        }

        public async Task<IEnumerable<T>> GetDataAsync<T>(string uri)
        {
            var address = Path.Combine(_host, uri).Replace("\\", "/");
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, address);
            var response = await _client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<T>>(result) ?? Enumerable.Empty<T>();
            }

            return Enumerable.Empty<T>();
        }

        public async Task<int> PostDataAsync<T>(string uri, T item)
        {
            var address = Path.Combine(_host, uri).Replace("\\", "/");
            var jsonData = JsonConvert.SerializeObject(item);
            var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, address)
            {
                Content = httpContent
            };

            var response = await _client.SendAsync(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<int>(result);
            }

            return 0;
        }
    }
}
