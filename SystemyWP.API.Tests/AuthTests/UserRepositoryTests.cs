using System.Threading.Tasks;
using SystemyWP.API.Policies;
using SystemyWP.API.Services.HttpServices;
using SystemyWP.API.Tests.Utilities;
using Xunit;

namespace SystemyWP.API.Tests.AuthTests;

public class UserRepositoryTests
{
    [Fact]
    public async Task UserCreationTest()
    {
        // Arrange
        using var masterApp = new DummyMasterApplication();
        using var gastronomyClient = masterApp.CreateClient();
        var policy = new HttpClientPolicy();
        var gastronomyHttpClient = new GastronomyHttpClient(gastronomyClient, policy);

        // Act
        var response = await gastronomyHttpClient.GetHealthCheckResponse();

        // Assert
        Assert.NotNull(response);
    }
}