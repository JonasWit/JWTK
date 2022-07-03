using System.Threading.Tasks;
using SystemyWP.API.Constants;
using SystemyWP.API.Data.DTOs.Gastronomy.Ingredients;
using SystemyWP.API.Policies;
using SystemyWP.API.Services.HttpServices;
using SystemyWP.API.Tests.Utilities;
using Xunit;

namespace SystemyWP.API.Tests.IntegrationServicesTests.GastronomyServiceTests;

public class HttpClientTests
{
    private const string _dummyAccessKey = "275a1184-8c4d-4063-9208-63e9747b217e";

    [Fact]
    public async Task CreateAndGetIngredientTest()
    {
        // Arrange
        using var gastronomyApp = new DummyGastronomyApplication();

        using var gastronomyClient = gastronomyApp.CreateClient();
        var gastronomyHttpClient = new GastronomyHttpClient(gastronomyClient, new HttpClientPolicy());

        var dto = new IngredientCreateDto
        {
            AccessKey = _dummyAccessKey,
            Name = "test12",
            Description = "test12",
            Category = "test category",
            MeasurementUnits = MeasurementUnits.Gram,
            StackSize = 1,
            PricePerStack = 1
        };

        // Act
        var response = await gastronomyHttpClient.CreateIngredient(dto);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(dto.Category, response.Category);
        Assert.Equal(dto.Description, response.Description);
        Assert.Equal(dto.Name, response.Name);
        Assert.Equal(dto.MeasurementUnits, response.MeasurementUnits);
        Assert.Equal(dto.StackSize, response.StackSize);
        Assert.Equal(dto.PricePerStack, response.PricePerStack);
    }
}