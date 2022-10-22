using MasterService.API.Data.DTOs.General.UserForms;
using MasterService.API.Services.JWTServices;
using MasterService.Tests.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace MasterService.Tests.AuthTests;

public class UserTests
{
    [Fact]
    public async Task UserCreationAndAuthenticationTest()
    {
        // Arrange
        using var masterApp = new DummyMasterApplication();
        using IServiceScope scope = masterApp.Services.CreateScope();
        TokenService tokenService = scope.ServiceProvider.GetRequiredService<TokenService>();
        using System.Net.Http.HttpClient client = masterApp.CreateClient();

        var form = new UserCredentialsForm
        {
            Email = "test@test.pl",
            Password = "Password1234567890"
        };

        // Act
        System.Net.Http.HttpResponseMessage response = await client.PostAsJsonAsync(@"auth\register", form);
        System.Net.Http.HttpResponseMessage authenticate = await client.PostAsJsonAsync(@"auth\authenticate", form);
        TokenObject token = await authenticate.Content.ReadFromJsonAsync<TokenObject>();

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