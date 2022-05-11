using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SystemyWP.API.Gastronomy.Data.DTOs;
using SystemyWP.API.Gastronomy.Data.DTOs.IngredientDTOs;
using SystemyWP.API.Gastronomy.Tests.Utilities;
using Xunit;

namespace SystemyWP.API.Gastronomy.Tests.ControllerTests;

public class ControllersTests
{
    [Fact]
    public async Task CreateAndGetIngredientTest()
    {
        // Arrange
        await using var application = new DummyGastronomyApplication();
        using var client = application.CreateClient();

        var newIngredient = new IngredientCreateDto
        {
            AccessKey = $"test-key-{nameof(CreateAndGetIngredientTest)}",
            Name = "Ingredient-1",
            Description = "Ingredient-1-Description",
            MeasurementUnits = MeasurementUnits.Gram,
            StackSize = 2,
            PricePerStack = 150
        };
        
        // Act
        var postResponse = await client.PostAsJsonAsync("/ingredient", newIngredient);
        var createdObject = await postResponse.Content.ReadFromJsonAsync<IngredientDto>();
        
        var getResponse = await client.GetAsync($"/ingredient/{createdObject?.AccessKey}/{createdObject?.Id}");
        var getObject = await getResponse.Content.ReadFromJsonAsync<IngredientDto>();
        
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
        Assert.NotNull(createdObject);
        Assert.Equal(createdObject?.AccessKey, newIngredient.AccessKey);      
        Assert.Equal(createdObject?.Name, newIngredient.Name);           
        Assert.Equal(createdObject?.Description, newIngredient.Description);    
        Assert.Equal(createdObject?.MeasurementUnits, newIngredient.MeasurementUnits);    
        Assert.Equal(createdObject?.StackSize, newIngredient.StackSize);    
        Assert.Equal(createdObject?.PricePerStack, newIngredient.PricePerStack);           
        
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.NotNull(getObject);
        Assert.Equal(getObject?.Id, createdObject?.Id);          
        Assert.Equal(getObject?.AccessKey, createdObject?.AccessKey);      
        Assert.Equal(getObject?.Name, createdObject?.Name);           
        Assert.Equal(getObject?.Description, createdObject?.Description);    
        Assert.Equal(getObject?.MeasurementUnits, createdObject?.MeasurementUnits);    
        Assert.Equal(getObject?.StackSize, createdObject?.StackSize);    
        Assert.Equal(getObject?.PricePerStack, createdObject?.PricePerStack);
    }
    
        [Fact]
    public async Task CreateAndGetMultipleIngredientsTest()
    {
        // Arrange
        await using var application = new DummyGastronomyApplication();
        using var client = application.CreateClient();

        var newIngredients = new List<IngredientCreateDto>();
        for (var i = 0; i < 20; i++)
        {
            newIngredients.Add(new IngredientCreateDto
            {
                AccessKey = $"test-key-{nameof(CreateAndGetMultipleIngredientsTest)}",
                Name = "Ingredient-1",
                Description = "Ingredient-1-Description",
                MeasurementUnits = MeasurementUnits.Gram,
                StackSize = 2,
                PricePerStack = 150
            });
        }
        
        // Act
        var postResponses = new List<HttpResponseMessage>();
        foreach (var ni in newIngredients)
        {
            var postResponse = await client.PostAsJsonAsync("/ingredient", ni);
            postResponses.Add(postResponse);             
        }

        var createdObjects = new List<IngredientDto>();
        foreach (var pr in postResponses)
        {
            createdObjects.Add(await pr.Content.ReadFromJsonAsync<IngredientDto>());
        }

        var getResponses = new List<HttpResponseMessage>(); 
        foreach (var co in createdObjects)
        {
            var getResponse = await client.GetAsync($"/ingredient/{co.AccessKey}/{co.Id}");
            getResponses.Add(getResponse);
        }

        var getObjects = new List<IngredientDto>();
        foreach (var gr in getResponses)
        {
            getObjects.Add(await gr.Content.ReadFromJsonAsync<IngredientDto>());
        }
        
        // Assert
        Assert.NotEmpty(postResponses);
        Assert.NotEmpty(getResponses);
        Assert.Equal(createdObjects.Count, getObjects.Count);
        Assert.All(postResponses, result => Assert.Equal(HttpStatusCode.OK, result.StatusCode));
        Assert.All(getResponses, result => Assert.Equal(HttpStatusCode.OK, result.StatusCode));
    }
}