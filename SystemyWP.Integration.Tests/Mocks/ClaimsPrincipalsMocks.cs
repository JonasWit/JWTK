using System.Collections.Generic;
using System.Security.Claims;
using SystemyWP.API;

namespace SystemyWP.Integration.Tests.Mocks
{
    public static class ClaimsPrincipalsMocks
    {
        public static ClaimsPrincipal CreateClientAdminPrincipal(string userId)
        {
            var claims = new List<Claim>() 
            { 
                new Claim(ClaimTypes.NameIdentifier, userId),
                SystemyWpConstants.Claims.LegalAppAccessClaim,
                SystemyWpConstants.Claims.ClientAdminClaim
            };
            var identity = new ClaimsIdentity(claims, "TestAdminClient");
            return new ClaimsPrincipal(identity);
        }
    }
}