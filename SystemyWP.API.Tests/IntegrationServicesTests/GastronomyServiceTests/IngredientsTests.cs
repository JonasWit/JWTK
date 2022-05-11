using System.Net.Http.Json;
using System.Threading.Tasks;
using SystemyWP.API.Constants;
using SystemyWP.API.Data.DTOs.Gastronomy.Ingredients;
using SystemyWP.API.Tests.Utilities;
using Xunit;

namespace SystemyWP.API.Tests.IntegrationServicesTests.GastronomyServiceTests;

public class IngredientsTests
{
    [Fact]
    public async Task CreateAndGetIngredientTest()
    {
        // Arrange
        await using var gastronomyApp = new DummyGastronomyApplication();
        await using var masterApp = new DummyMasterApplication();
        using var masterClient = masterApp.CreateClient();

        var newIngredient = new IngredientCreateDto
        {
            AccessKey = "abc",
            Name = "test12",
            Description = "test12",
            MeasurementUnits = MeasurementUnits.Gram,
            StackSize = 1,
            PricePerStack = 120
        };
        
        // Act
        var postResponse = await masterClient.PostAsJsonAsync("/GastronomyIngredients", newIngredient);
    
        
    
        
        
        // Assert
    
    }
}