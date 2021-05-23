using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SystemyWP.Integration.Tests.Mocks
{
    public class UnauthorizedAuthMock: AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public UnauthorizedAuthMock(IOptionsMonitor<AuthenticationSchemeOptions> options,
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
            };
            
            var identity = new ClaimsIdentity(claims, "Test SWP");
            var principal = new ClaimsPrincipal(new[] { identity });
            var ticket = new AuthenticationTicket(principal, "Unauthorized");
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}