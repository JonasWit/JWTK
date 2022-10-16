using Blazored.LocalStorage;
using System.IdentityModel.Tokens.Jwt;
using VappsWeb.Config;
using VappsWeb.Models;
using VappsWeb.Services.Interfaces;

namespace VappsWeb.Services
{
    public class TokenService : ITokenService
    {
        private readonly ILocalStorageService _localStorage;

        public TokenService(ILocalStorageService localStorage) => _localStorage = localStorage;

        public JwtSecurityToken? ExtractStoredToken() => throw new NotImplementedException();

        public JwtSecurityToken ExtractToken(AuthorizeResponse token) => throw new NotImplementedException();
        public ValueTask<AuthorizeResponse> GetTokenFromStore() => _localStorage.GetItemAsync<AuthorizeResponse>(AppConfig.LocalStoreItems.AuthorizationToken);
        public ValueTask StoreToken(AuthorizeResponse token) => _localStorage.SetItemAsync(AppConfig.LocalStoreItems.AuthorizationToken, token);

        public bool ValidateStoredToken() => throw new NotImplementedException();
        public bool ValidateToken(AuthorizeResponse token) => throw new NotImplementedException();

    }
}
