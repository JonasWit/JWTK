using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API;
using SystemyWP.API.Repositories.LegalApp;
using SystemyWP.Integration.Tests.Mocks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace SystemyWP.Integration.Tests.RepositoriesTests
{
    public class LegalAppClientRepositoryTests : IClassFixture<AppInstance>
    {
        private readonly AppInstance _instance;

        public LegalAppClientRepositoryTests(AppInstance instance)
        {
            _instance = instance;
        }

        [Fact]
        public async Task LegalAppClients_CheckClientAdminAccess_GetBasicClientsList()
        {
            var scope = _instance.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var repository = scope.ServiceProvider.GetRequiredService<LegalAppClientRepository>();
            
            var adminUser = userManager
                .GetUsersForClaimAsync(SystemyWpConstants.Claims.ClientAdminClaim)
                .GetAwaiter()
                .GetResult()
                .FirstOrDefault(x => x.Email.Equals(TestsConstants.Emails.ClientAdminEmail, StringComparison.InvariantCultureIgnoreCase));

            var claimsPrincipal = ClaimsPrincipalsMocks.CreateClientAdminPrincipal(adminUser?.Id);
            var result = await repository.GetClientsBasicList(claimsPrincipal);

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Result.Should().NotBeEmpty().And.HaveCount(100);
        }
        
        [Fact]
        public async Task LegalAppClients_CheckClientAdminAccess_GetClientsList()
        {
            var scope = _instance.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var repository = scope.ServiceProvider.GetRequiredService<LegalAppClientRepository>();
            
            var adminUser = userManager
                .GetUsersForClaimAsync(SystemyWpConstants.Claims.ClientAdminClaim)
                .GetAwaiter()
                .GetResult()
                .FirstOrDefault(x => x.Email.Equals(TestsConstants.Emails.ClientAdminEmail, StringComparison.InvariantCultureIgnoreCase));

            var claimsPrincipal = ClaimsPrincipalsMocks.CreateClientAdminPrincipal(adminUser?.Id);
            var result = await repository.GetClients(claimsPrincipal, 0, 10);

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Result.Should().NotBeEmpty().And.HaveCount(10);
        }
    }
}