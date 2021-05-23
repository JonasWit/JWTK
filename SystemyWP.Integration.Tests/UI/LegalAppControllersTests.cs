using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.LegalApp.Client;
using SystemyWP.API.Forms.LegalApp.Client;
using SystemyWP.API.Forms.Validation.LegalApp.Client;
using SystemyWP.Integration.Tests.Fixtures;
using SystemyWP.Integration.Tests.Utilities;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Xunit;

namespace SystemyWP.Integration.Tests.UI
{
    public class LegalAppControllersTests : IClassFixture<SystemyWPFixture>
    {
        private readonly SystemyWPFixture _factory;

        public LegalAppControllersTests(SystemyWPFixture factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetBasicClientsList()
        {
            var client = _factory.CreateClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TestConstants.AuthenticationSchemes.ClientAdminTest);

            var request = new HttpRequestMessage(HttpMethod.Get, "/api/legal-app-clients/clients/basic-list");
            var response = await client.SendAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
        [Fact]
        public async Task GetClientsList()
        {
            var client = _factory.CreateClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TestConstants.AuthenticationSchemes.UnauthorizedTest);

            var request = new HttpRequestMessage(HttpMethod.Get, "/api/legal-app-clients/clients");
            var response = await client.SendAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
        [Fact]
        public async Task CreateClient()
        {
            var client = _factory.CreateClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TestConstants.AuthenticationSchemes.ClientAdminTest);

            var validator = new CreateClientFormValidation();
            var payload = new CreateClientForm() {Name = "test"};
            var validationResult = validator.Validate(payload);

            var response = await client.PostAsync("/api/legal-app-clients/create", 
                new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"));

            Assert.True(validationResult.IsValid);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
        [Fact]
        public async Task GetClient()
        {
            var client = _factory.CreateClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TestConstants.AuthenticationSchemes.ClientAdminTest);

            var request = new HttpRequestMessage(HttpMethod.Get, "/api/legal-app-clients/client/1");
            var response = await client.SendAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
        [Fact]
        public async Task ArchiveClient()
        {
            var client = _factory.CreateClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TestConstants.AuthenticationSchemes.ClientAdminTest);

            var request = new HttpRequestMessage(HttpMethod.Put, "/api/legal-app-clients/archive/1");
            var response = await client.SendAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
    }
}