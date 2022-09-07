using VappsMobile.AppConfig;
using VappsMobile.Models;
using VappsMobile.Policies;

namespace VappsMobile.Services
{
    public class AuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClientPolicy _httpClientPolicy;
        private readonly string _url;

        public AuthService(
            IHttpClientFactory httpClientFactory,
            HttpClientPolicy httpClientPolicy)
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

        public async Task<UserInfo> SignIn(string email, string password)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {

                HttpClient client = GetClient(AppConstants.HttpClientsNames.AuthHttpClient);

                HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(() => client.GetAsync("health"));

            }
            return null;
        }

        public bool IsSignedIn()
        {

            return true;
        }
    }
}
