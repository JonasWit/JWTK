using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore.Query.Internal;
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