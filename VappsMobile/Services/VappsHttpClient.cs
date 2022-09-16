using VappsMobile.AppConfig;
using VappsMobile.Policies;

namespace VappsMobile.Services
{
    public class VappsHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClientPolicy _httpClientPolicy;
        private readonly string _url;

        public VappsHttpClient(IHttpClientFactory httpClientFactory, HttpClientPolicy httpClientPolicy)
        {
            _httpClientFactory = httpClientFactory;
            _httpClientPolicy = httpClientPolicy;
            _url = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
        }

        public HttpClient GetClient(string name)
        {
            HttpClient client = _httpClientFactory.CreateClient(name);
            client.BaseAddress = new Uri(_url);
            return client;
        }

        public async Task HealthCheck()
        {
            HttpClient client = GetClient(ApiConfig.HttpClientsNames.AuthClient);

            HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(() => client.GetAsync("health"));

        }
    }
}
