using System;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using SystemyWP.API;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace SystemyWP.Integration.Tests.LegalAppTests.Clients
{
    public class LegalAppClientsTests : IClassFixture<AppInstance>
    {
        private readonly AppInstance _instance;

        public LegalAppClientsTests(AppInstance instance)
        {
            _instance = instance;
        }

        [Fact]
        public async Task LegalAppClients_GetBasicClientsList_AuthorizedClientAdmin()
        {
            var scope = _instance.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var user = userManager
                .GetUsersForClaimAsync(SystemyWpConstants.Claims.ClientAdminClaim)
                .GetAwaiter()
                .GetResult()
                .First(x =>
                    x.Email.Equals(TestsConstants.Emails.ClientAdminEmail,
                        StringComparison.InvariantCultureIgnoreCase));

            var client = _instance
                .AuthenticatedInstance(
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    SystemyWpConstants.Claims.LegalAppAccessClaim,
                    SystemyWpConstants.Claims.ClientAdminClaim
                )
                .CreateClient(new WebApplicationFactoryClientOptions()
                {
                    AllowAutoRedirect = false,
                });

            var result = await client.GetAsync("/api/legal-app-clients/clients/basic-list");

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            var content = await result.Content.ReadAsStringAsync();
            content.Should().NotBeEmpty();
        }

        [Fact]
        public async Task LegalAppClients_GetClient_UnauthorizedClientAdmin()
        {
            var scope = _instance.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var user = userManager
                .GetUsersForClaimAsync(SystemyWpConstants.Claims.ClientAdminClaim)
                .GetAwaiter()
                .GetResult()
                .First(x =>
                    x.Email.Equals(TestsConstants.Emails.ClientAdminEmail,
                        StringComparison.InvariantCultureIgnoreCase));

            var client = _instance
                .AuthenticatedInstance(
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    SystemyWpConstants.Claims.LegalAppAccessClaim,
                    SystemyWpConstants.Claims.ClientAdminClaim
                )
                .CreateClient(new WebApplicationFactoryClientOptions()
                {
                    AllowAutoRedirect = false,
                });

            var result = await client.GetAsync("/api/legal-app-clients/client/300");

            result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task LegalAppClients_GetBasicClientsList_UnauthorizedClient()
        {
            var scope = _instance.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var user = userManager
                .GetUsersForClaimAsync(SystemyWpConstants.Claims.ClientAdminClaim)
                .GetAwaiter()
                .GetResult()
                .First(x =>
                    x.Email.Equals(TestsConstants.Emails.ClientAdminEmail,
                        StringComparison.InvariantCultureIgnoreCase));

            var clientWithLegalAppClaim = _instance
                .AuthenticatedInstance(
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    SystemyWpConstants.Claims.LegalAppAccessClaim,
                    SystemyWpConstants.Claims.ClientClaim
                )
                .CreateClient(new WebApplicationFactoryClientOptions()
                {
                    AllowAutoRedirect = false,
                });

            var clientWithoutLegalAppClaim = _instance
                .AuthenticatedInstance(
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    SystemyWpConstants.Claims.ClientClaim
                )
                .CreateClient(new WebApplicationFactoryClientOptions()
                {
                    AllowAutoRedirect = false,
                });

            var resultWithAppClaim =
                await clientWithLegalAppClaim.GetAsync("/api/legal-app-clients/clients/basic-list");

            var resultWithoutLegalAppClaim =
                await clientWithoutLegalAppClaim.GetAsync("/api/legal-app-clients/clients/basic-list");

            resultWithAppClaim.StatusCode.Should().Be(StatusCodes.Status200OK);
            (await resultWithAppClaim.Content.ReadFromJsonAsync<object[]>()).Should().HaveCount(0);

            resultWithoutLegalAppClaim.StatusCode.Should().Be(StatusCodes.Status403Forbidden);
        }
    }
}