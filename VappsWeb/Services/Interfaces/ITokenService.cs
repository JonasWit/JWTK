using System.IdentityModel.Tokens.Jwt;
using VappsWeb.Models;

namespace VappsWeb.Services.Interfaces
{
    public interface ITokenService
    {
        ValueTask<AuthorizeResponse> GetTokenFromStore();
        ValueTask StoreToken(AuthorizeResponse token);
        bool ValidateToken(AuthorizeResponse token);
        bool ValidateStoredToken();
        JwtSecurityToken ExtractToken(AuthorizeResponse token);
        JwtSecurityToken? ExtractStoredToken();
    }
}
