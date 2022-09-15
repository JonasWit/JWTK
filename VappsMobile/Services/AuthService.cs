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
        private readonly UserService _userService;

        public HttpClient Client => _httpClientFactory.CreateClient(ApiConfig.HttpClientsNames.AuthClient);

        public AuthService(IHttpClientFactory httpClientFactory, HttpClientPolicy httpClientPolicy, UserService userService)
        {
            _httpClientFactory = httpClientFactory;
            _httpClientPolicy = httpClientPolicy;
            _userService = userService;
        }

        public async Task<bool> GetStoredUser()
        {
            var token = await SecureStorage.GetAsync(AppConstants.StoredPreferenceName.JWTToken);
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            _userService.UserInfo = new UserInfo(token);
            if (_userService.UserInfo.Expire <= DateTime.UtcNow)
            {
                _ = SecureStorage.Remove(AppConstants.StoredPreferenceName.JWTToken);
                return false;
            }

            return true;
        }

        public bool SignOut()
        {
            try
            {
                if (SecureStorage.Remove(AppConstants.StoredPreferenceName.JWTToken))
                {
                    _userService.UserInfo = null;
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAccount()
        {
            try
            {
                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                {
                    return false;
                }

                HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(() => Client.DeleteAsync(ApiConfig.ApiAuthController.DeleteAccount));
                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SignUp(string email, string password)
        {
            try
            {
                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                {
                    return false;
                }

                HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(() => Client.PostAsJsonAsync(ApiConfig.ApiAuthController.Register, new { Email = email, Password = password }));

                return response.StatusCode == System.Net.HttpStatusCode.OK;
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

                _userService.UserInfo = new UserInfo(token.Token);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
