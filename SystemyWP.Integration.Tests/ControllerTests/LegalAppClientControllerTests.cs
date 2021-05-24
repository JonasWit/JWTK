using System.Net;
using System.Threading.Tasks;
using SystemyWP.Integration.Tests.BaseIntegrationTests;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Xunit;

namespace SystemyWP.Integration.Tests.ControllerTests
{
    public class LegalAppClientControllerTests : IntegrationTests
    {
        [Fact]
        public async Task GetClientsBasicList_GetListWithoutPermission()
        {
            // Arrange
            //AuthenticateAsClientAdmin();

            // Act
            var response = await TestClient.GetAsync("/api/legal-app-clients/clients/basic-list");
            
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            (await response.Content.ReadAsStringAsync()).Should().BeEmpty();
        }
    }
}