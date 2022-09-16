using System.Net.Http.Json;
using VappsMobile.AppConfig;
using VappsMobile.Models.AuthModels;
using VappsMobile.Policies;

namespace VappsMobile.Services
{
    public class HealthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClientPolicy _httpClientPolicy;

        private HttpClient GetClient => _httpClientFactory.CreateClient(ApiConfig.HttpClientsNames.HealthClient);

        public HealthService(IHttpClientFactory httpClientFactory, HttpClientPolicy httpClientPolicy)
        {
            _httpClientFactory = httpClientFactory;
            _httpClientPolicy = httpClientPolicy;
        }

        public async Task<HealthCheckResponse> CheckHealth()
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                return null;
            }

            Thread.Sleep(1000);

            HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(() => GetClient.GetAsync("health"));
            HealthCheckResponse res = !response.IsSuccessStatusCode ? throw new Exception() : await response.Content.ReadFromJsonAsync<HealthCheckResponse>();

            return res;
        }
    }
}
