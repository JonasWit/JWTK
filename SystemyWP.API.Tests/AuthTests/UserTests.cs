using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SystemyWP.API.Data.DTOs.General.UserForms;
using SystemyWP.API.Services.JWTServices;
using SystemyWP.API.Tests.Utilities;
using Xunit;

namespace SystemyWP.API.Tests.AuthTests;

public class UserTests
{
    [Fact]
    public async Task UserCreationAndAuthenticationTest()
    {
        // Arrange
        using var masterApp = new DummyMasterApplication();
        using var scope = masterApp.Services.CreateScope();
        var tokenService = scope.ServiceProvider.GetRequiredService<TokenService>();
        using var client = masterApp.CreateClient();

        var form = new UserCredentialsForm
        {
            Email = "test@test.pl",
            Password = "Password1234567890"
        };

        // Act
        var response = await client.PostAsJsonAsync(@"auth\register", form);
        var authenticate = await client.PostAsJsonAsync(@"auth\authenticate", form);
        var token = await authenticate.Content.ReadFromJsonAsync<TokenObject>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(HttpStatusCode.OK, authenticate.StatusCode);
        Assert.True(tokenService.ValidateToken(token.Token));
    }

    public struct TokenObject
    {
        public string Token { get; set; }
    }
}