using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SystemyWP.API;
using SystemyWP.Data;
using SystemyWP.Integration.Tests.Infrastructure;
using SystemyWP.Integration.Tests.Mocks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SystemyWP.Integration.Tests.BaseIntegrationTests
{
    public class IntegrationTests
    {
        protected readonly HttpClient TestClient;

        public IntegrationTests()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        // services.AddAuthentication("CA-Test")
                        //     .AddScheme<AuthenticationSchemeOptions, ClientAdminMock>("CA-Test", _ => { });
                        
                        services.RemoveAll(typeof(AppDbContext));
                        services.AddDbContext<AppDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDb");
                        });
                        services.AddDbContext<ApiIdentityDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDb");
                        });

                        DbContextUtils.SeedDatabase(services.BuildServiceProvider());
                    });
                });
            TestClient = appFactory.CreateClient();
        }
        
        protected void AuthenticateAsClientAdmin()
        {
            TestClient.DefaultRequestHeaders.Authorization =  
                new AuthenticationHeaderValue("CA-Test");
        }
    }
}