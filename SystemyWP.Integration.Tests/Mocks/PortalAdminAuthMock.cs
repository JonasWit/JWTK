using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using SystemyWP.API;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SystemyWP.Integration.Tests.Mocks
{
    public class PortalAdminAuthMock: AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public PortalAdminAuthMock(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, "test@test.com"),
                SystemyWpConstants.Claims.PortalAdminClaim,
                SystemyWpConstants.Claims.LegalAppAccessClaim, 
            };
            
            var identity = new ClaimsIdentity(claims, "Test SWP");
            var principal = new ClaimsPrincipal(new[] { identity });
            var ticket = new AuthenticationTicket(principal, "PortalAdmin");
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}