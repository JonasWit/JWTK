﻿using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SystemyWP.API;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace SystemyWP.Integration.Tests.PortalTests
{
    public class UserTests: IClassFixture<AppInstance>
    {
        private readonly AppInstance _instance;

        public UserTests(AppInstance instance)
        {
            _instance = instance;
        }
        
        [Fact]
        public async Task PortalUsers_GetProfile_AuthorizedProfileRequest()
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

            var result = await client.GetAsync("/api/users/me");

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            var content = await result.Content.ReadAsStringAsync();
            content.Should().NotBeEmpty();
        }




    }
}