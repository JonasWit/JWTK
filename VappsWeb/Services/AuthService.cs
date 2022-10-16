using System.Net.Http.Json;
using VappsWeb.Config;
using VappsWeb.Models;
using VappsWeb.Services.Interfaces;

namespace VappsWeb.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClientPolicy _httpClientPolicy;
        private readonly ITokenService _tokenService;

        public AuthService(HttpClient httpClient, HttpClientPolicy httpClientPolicy, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _httpClientPolicy = httpClientPolicy;
            _tokenService = tokenService;
        }

        public async Task<bool> SignIn(string email, string password)
        {
            try
            {
                HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(() =>
                    _httpClient.PostAsJsonAsync(AppConfig.ApiRoutes.LoginPath, new { Email = email, Password = password }));
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return false;
                }

                AuthorizeResponse? token = await response.Content.ReadFromJsonAsync<AuthorizeResponse>();
                if (token is null)
                {
                    return false;
                }

                await _tokenService.StoreToken(token);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
