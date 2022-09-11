using System.Net.Http.Json;
using VappsMobile.AppConfig;
using VappsMobile.Models.AuthModels;
using VappsMobile.Policies;

namespace VappsMobile.Services
{
    public class AuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClientPolicy _httpClientPolicy;

        public UserInfo UserInfo { get; set; }
        public HttpClient Client => _httpClientFactory.CreateClient(ApiConfig.HttpClientsNames.AuthClient);

        public AuthService(
            IHttpClientFactory httpClientFactory,
            HttpClientPolicy httpClientPolicy)
        {
            _httpClientFactory = httpClientFactory;
            _httpClientPolicy = httpClientPolicy;
        }

        public async Task<bool> GetStoredUser()
        {
            var token = await SecureStorage.GetAsync(AppConstants.StoredPreferenceName.JWTToken);
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            UserInfo = new UserInfo(token);
            return true;
        }

        public bool SignOut()
        {
            try
            {
                _ = SecureStorage.Remove(AppConstants.StoredPreferenceName.JWTToken);
                UserInfo = null;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SignIn(string email, string password, bool remember)
        {
            try
            {
                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                {
                    return false;
                }

                HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(() => Client.PostAsJsonAsync(ApiConfig.ApiAuthController.Authenticate, new { Email = email, Password = password }));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return false;
                }

                AuthorizeResponse token = await response.Content.ReadFromJsonAsync<AuthorizeResponse>();

                if (remember)
                {
                    _ = SecureStorage.Remove(AppConstants.StoredPreferenceName.JWTToken);
                    await SecureStorage.SetAsync(AppConstants.StoredPreferenceName.JWTToken, token.Token);
                }

                UserInfo = new UserInfo(token.Token);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
