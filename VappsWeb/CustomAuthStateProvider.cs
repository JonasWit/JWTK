using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using VappsWeb.Models;
using VappsWeb.Services.Interfaces;

namespace VappsWeb
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ITokenService _tokenService;
        private readonly HttpClient _http;

        public CustomAuthStateProvider(ITokenService tokenService, HttpClient http)
        {
            _tokenService = tokenService;
            _http = http;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            AuthorizeResponse? authResponse = await _tokenService.GetTokenFromStore();
            var identity = new ClaimsIdentity();

            if (authResponse is null)
            {
                _http.DefaultRequestHeaders.Authorization = null;
                var cp = new ClaimsPrincipal(identity);
                var st = new AuthenticationState(cp);

                NotifyAuthenticationStateChanged(Task.FromResult(st));
                return st;
            }

            JwtSecurityToken securityToken = new JwtSecurityTokenHandler().ReadJwtToken(authResponse.Token);
            _ = securityToken.ValidTo;

            identity = new ClaimsIdentity(securityToken.Claims, "jwt");
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", authResponse.Token);

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }
    }
}
