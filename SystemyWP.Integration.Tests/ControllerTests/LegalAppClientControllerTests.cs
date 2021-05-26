using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SystemyWP.API;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace SystemyWP.Integration.Tests.ControllerTests
{
    public class LegalAppClientControllerTests : IClassFixture<AppInstance>
    {
        private readonly AppInstance _instance;

        public LegalAppClientControllerTests(AppInstance instance)
        {
            _instance = instance;
        }
        
        [Fact]
        public async Task LegalAppClients_GetClient()
        {
            var userManager = _instance.Services.GetRequiredService<UserManager<IdentityUser>>();
            var adminUsers = userManager
                .GetUsersForClaimAsync(SystemyWpConstants.Claims.ClientAdminClaim)
                .GetAwaiter()
                .GetResult();

            var lastAdmin = adminUsers.FirstOrDefault(x =>
                x.Email.Equals("clientadmin7@test.com", StringComparison.InvariantCultureIgnoreCase));
            
         
            // Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            // var content = result.Content.ReadAsStringAsync();
        }

        [Fact]
        public async Task PublicTest()
        {
            
            
            var client = _instance
                .AuthenticatedInstance()
                .CreateClient(new()
                {
                    AllowAutoRedirect = false
                });

            var result = await client
                .GetAsync("/api/legal-app-clients/clients/basic-list");

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var content = result.Content.ReadAsStringAsync();
        }
    }
}