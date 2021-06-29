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
        public async Task LegalAppClients_GetBasicClientsList_AuthorizedUserAdmin()
        {
            var scope = _instance.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var user = userManager
                .GetUsersForClaimAsync(SystemyWpConstants.Claims.UserAdminClaim)
                .GetAwaiter()
                .GetResult()
                .First(x =>
                    x.Email.Equals(TestsConstants.Emails.UserAdminEmailKey2,
                        StringComparison.InvariantCultureIgnoreCase));

            var client = _instance
                .AuthenticatedInstance(
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    SystemyWpConstants.Claims.LegalAppAccessClaim,
                    SystemyWpConstants.Claims.UserAdminClaim
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
        public async Task LegalAppClients_GetClient_UnauthorizedUserAdmin()
        {
            var scope = _instance.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var user = userManager
                .GetUsersForClaimAsync(SystemyWpConstants.Claims.UserAdminClaim)
                .GetAwaiter()
                .GetResult()
                .First(x =>
                    x.Email.Equals(TestsConstants.Emails.UserAdminEmailKey2,
                        StringComparison.InvariantCultureIgnoreCase));

            var client = _instance
                .AuthenticatedInstance(
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    SystemyWpConstants.Claims.LegalAppAccessClaim,
                    SystemyWpConstants.Claims.UserAdminClaim
                )
                .CreateClient(new WebApplicationFactoryClientOptions()
                {
                    AllowAutoRedirect = false,
                });

            var result = await client.GetAsync("/api/legal-app-clients/client/251");

            result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task LegalAppClients_GetBasicClientsList_UnauthorizedUser()
        {
            var scope = _instance.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var user = userManager
                .GetUsersForClaimAsync(SystemyWpConstants.Claims.UserAdminClaim)
                .GetAwaiter()
                .GetResult()
                .First(x =>
                    x.Email.Equals(TestsConstants.Emails.UserAdminEmailKey2,
                        StringComparison.InvariantCultureIgnoreCase));

            var clientWithLegalAppClaim = _instance
                .AuthenticatedInstance(
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    SystemyWpConstants.Claims.LegalAppAccessClaim,
                    SystemyWpConstants.Claims.UserClaim
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
                    SystemyWpConstants.Claims.UserClaim
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

        [Fact]
        public async Task LegalAppClients_GetClient_UserWithoutPermission()
        {
            var scope = _instance.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var user = userManager
                .GetUsersForClaimAsync(SystemyWpConstants.Claims.UserClaim)
                .GetAwaiter()
                .GetResult()
                .First(x =>
                    x.Email.Equals(TestsConstants.Emails.User2EmailKey1,
                        StringComparison.InvariantCultureIgnoreCase));

            var httpClient = _instance
                .AuthenticatedInstance(
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    SystemyWpConstants.Claims.LegalAppAccessClaim,
                    SystemyWpConstants.Claims.UserClaim
                )
                .CreateClient(new WebApplicationFactoryClientOptions()
                {
                    AllowAutoRedirect = false,
                });

            var differentKeyClient = await httpClient.GetAsync("/api/legal-app-clients/client/1");
            var sameKeyClient = await httpClient.GetAsync("/api/legal-app-clients/client/217");

            differentKeyClient.StatusCode.Should().Be(StatusCodes.Status204NoContent);
            sameKeyClient.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task LegalAppClients_GetClient_UserAdminWithoutKey()
        {
            var scope = _instance.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var user = userManager
                .GetUsersForClaimAsync(SystemyWpConstants.Claims.UserAdminClaim)
                .GetAwaiter()
                .GetResult()
                .First(x =>
                    x.Email.Equals(TestsConstants.Emails.UserAdminEmailNoKey,
                        StringComparison.InvariantCultureIgnoreCase));

            var httpClient = _instance
                .AuthenticatedInstance(
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    SystemyWpConstants.Claims.LegalAppAccessClaim,
                    SystemyWpConstants.Claims.UserAdminClaim
                )
                .CreateClient(new WebApplicationFactoryClientOptions()
                {
                    AllowAutoRedirect = false,
                });

            var clientCall1 = await httpClient.GetAsync("/api/legal-app-clients/client/1");
            var clientCall2 = await httpClient.GetAsync("/api/legal-app-clients/client/217");

            clientCall1.StatusCode.Should().Be(StatusCodes.Status204NoContent);
            clientCall2.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }
    }
}