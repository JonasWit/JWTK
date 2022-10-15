using System.IdentityModel.Tokens.Jwt;

namespace BlazorWASMSandbox.Services.Interfaces
{
    public interface ITokenService
    {
        string GetTokenFromStore();
        bool StoreToken(string token);
        bool ValidateToken(string token);
        bool ValidateStoredToken();
        JwtSecurityToken ExtractToken(string token);
        JwtSecurityToken? ExtractStoredToken();
    }
}
