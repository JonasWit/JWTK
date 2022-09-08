using VappsMobile.AppConfig;
using VappsMobile.Models;
using VappsMobile.Policies;

namespace VappsMobile.Services
{
    public class AuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClientPolicy _httpClientPolicy;

        public AuthService(
            IHttpClientFactory httpClientFactory,
            HttpClientPolicy httpClientPolicy)
        {
            _httpClientFactory = httpClientFactory;
            _httpClientPolicy = httpClientPolicy;
        }

        public void GetStoredUser()
        {

        }

        private HttpClient GetClient(string name)
        {
            HttpClient client = _httpClientFactory.CreateClient(name);
            return client;
        }

        public async Task<UserInfo> SignIn(string email, string password)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {

                HttpClient client = GetClient(ApiConfig.HttpClientsNames.AuthClient);

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
