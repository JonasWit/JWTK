using System.Threading.Tasks;
using SystemyWP.API.Constants;
using SystemyWP.API.Data.DTOs.Gastronomy.Ingredients;
using SystemyWP.API.Policies;
using SystemyWP.API.Services.HttpServices;
using SystemyWP.API.Tests.Utilities;
using Xunit;

namespace SystemyWP.API.Tests.IntegrationServicesTests.GastronomyServiceTests;

public class IngredientsTests
{
    [Fact]
    public async Task CreateAndGetIngredientTest()
    {
        // Arrange
        // Arrange
        await using var gastronomyApp = new DummyGastronomyApplication();
        using var gastronomyClient = gastronomyApp.CreateClient();
        var policy = new HttpClientPolicy();
        var gastronomyHttpClient = new GastronomyHttpClient(gastronomyClient, policy);

        var newIngredient = new IngredientCreateDto
        {
            AccessKey = "abc",
            Name = "test12",
            Description = "test12",
            MeasurementUnits = MeasurementUnits.Gram,
            Category = "test category",
            StackSize = 1,
            PricePerStack = 120
        };

        // Act
        var response = await gastronomyHttpClient.CreateIngredient(newIngredient);

        // Assert
        Assert.NotNull(response);


        // Act


        // Assert
    }
}