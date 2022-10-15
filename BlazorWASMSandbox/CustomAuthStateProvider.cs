using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace BlazorWASMSandbox
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
            var token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ3aXRlay5qODdAZ21haWwuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IndpdGVrLmo4N0BnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJVc2VyIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiI5M2YzMTU2My05M2NiLTQwMTQtODIxYi00ZWYzODljMWEzODgiLCJuYmYiOjE2NjU1OTUwNzksImV4cCI6MTY2NjgwNDY3OSwiaXNzIjoiZGV2LXNlcnZlciIsImF1ZCI6ImRldi1zZXJ2ZXIifQ.9YNnhZkv9pCpUpOf0lAjrpe_hBLTC6Cj-Gn6_-_-2F2I_KZglGm_esCC4nK-dZzXwEXC8cyTWGDuU6GsQ9VHNg";

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
