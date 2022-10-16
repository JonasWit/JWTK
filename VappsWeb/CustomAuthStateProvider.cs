using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace VappsWeb
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;

        public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            var token = string.Empty;

            var identity = new ClaimsIdentity();
            _http.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(token))
            {
                JwtSecurityToken securityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                _ = securityToken.ValidTo;

                identity = new ClaimsIdentity(securityToken.Claims, "jwt");
                _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            }

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return Task.FromResult(state);

            //var identity = new ClaimsIdentity();
            //_http.DefaultRequestHeaders.Authorization = null;

            //if (!string.IsNullOrEmpty(token))
            //{
            //    identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            //    _http.DefaultRequestHeaders.Authorization =
            //        new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            //}

            //var user = new ClaimsPrincipal(identity);
            //var state = new AuthenticationState(user);

            //NotifyAuthenticationStateChanged(Task.FromResult(state));

            //return Task.FromResult(state);
        }
    }
}
