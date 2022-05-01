using System.Net.Http.Json;
using System.Threading.Tasks;
using SystemyWP.API.Data.DTOs.General;
using SystemyWP.API.Tests.Utilities;
using Xunit;

namespace SystemyWP.API.Tests.IntegrationServicesTests;

public class MaintenanceCheck
{
    [Fact]
    public async Task CreateAndGetIngredientTest()
    {
        // Arrange
        await using var gastronomyApp = new DummyGastronomyApplication();
        await using var masterApp = new DummyMasterApplication();
        
        using var gastronomyClient = gastronomyApp.CreateClient();
        using var masterClient = masterApp.CreateClient();
        
        // Act
        var postResponse = await masterClient.GetFromJsonAsync<HealthCheckDto>("/health");

        

        
        
        // Assert

    }
}