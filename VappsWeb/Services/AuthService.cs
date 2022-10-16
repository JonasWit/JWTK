using Blazored.LocalStorage;
using System.Net.Http.Json;
using VappsWeb.Config;
using VappsWeb.Models;

namespace VappsWeb.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClientPolicy _httpClientPolicy;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, HttpClientPolicy httpClientPolicy, ILocalStorageService LocalStorage)
        {
            _httpClient = httpClient;
            _httpClientPolicy = httpClientPolicy;
            _localStorage = LocalStorage;
        }

        public async Task<bool> SignIn(string email, string password)
        {
            try
            {
                HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(() => _httpClient.PostAsJsonAsync(AppConfig.ApiRoutes.LoginPath, new { Email = email, Password = password }));
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return false;
                }

                AuthorizeResponse? token = await response.Content.ReadFromJsonAsync<AuthorizeResponse>();
                if (token is null)
                {
                    return false;
                }

                await _localStorage.SetItemAsync(AppConfig.LocalStoreItems.AuthorizationToken, token);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
