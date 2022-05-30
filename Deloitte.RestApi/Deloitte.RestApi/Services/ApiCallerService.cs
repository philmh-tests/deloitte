using System.Net.Http;
using System.Threading.Tasks;
using Deloitte.RestApi.Services.Contracts;

namespace Deloitte.RestApi.Services
{
    public class ApiCallerService : IApiCallerService
    {
        private readonly HttpClient _httpClient;

        public ApiCallerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAsync(string requestUri)
        {
            var httpResponseMessage = await _httpClient.GetAsync(requestUri);
            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
    }
}