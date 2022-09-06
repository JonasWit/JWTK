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

        public Task<UserInfo> SignIn(string email, string password)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {

            }
            return Task.FromResult(new UserInfo("token"));
        }

        public bool IsSignedIn()
        {
            return true;
        }
    }
}
