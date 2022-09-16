using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace VappsMobile.Models.AuthModels
{
    public class UserInfo
    {
        private readonly JwtSecurityToken _securityToken;
        public string Token { get; private set; }

        public string Email => _securityToken.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Name)).Value;
        public string Role => _securityToken.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role)).Value;
        public DateTime Expire => _securityToken.ValidTo;

        public UserInfo(string token)
        {
            _securityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            Token = token;
        }
    }
}
