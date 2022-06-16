using System.Threading.Tasks;
using SystemyWP.API.Constants;
using SystemyWP.API.Policies;
using SystemyWP.API.Services.HttpServices;
using SystemyWP.API.Tests.Utilities;
using Xunit;

namespace SystemyWP.API.Tests.IntegrationServicesTests;

public class MaintenanceCheck
{
    [Fact]
    public async Task HealthCheckAliveResponseTest()
    {
        // Arrange
        await using var gastronomyApp = new DummyGastronomyApplication();
        using var gastronomyClient = gastronomyApp.CreateClient();
        var policy = new HttpClientPolicy();
        var gastronomyHttpClient = new GastronomyHttpClient(gastronomyClient, policy);

        // Act
        var response = await gastronomyHttpClient.GetHealthCheckResponse();

        // Assert
        Assert.NotNull(response);
        Assert.Equal(AppConstants.ServiceResponses.AliveResponse, response);
    }
}