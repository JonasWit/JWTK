using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Xunit;

namespace SystemyWP.Tests.GastronomyTests.IntegrationTests;

public class ControllersTests
{

    
    [Fact]
    public async Task CreateIngredientTest()
    {
        // Arrange
        await using var application = new MockedGastronomyApplication();
        using var client = application.CreateClient();
        
        
        var jsonString = "{\"test\":123}";
        
      
        using var jsonContent = new StringContent(jsonString);
        
        // Act
        jsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

 
        using var response = await client.PostAsync("/jsonraw", jsonContent);

        
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        
   
        
        
        
        
    }
}