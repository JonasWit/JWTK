using SystemyWP.API;
using SystemyWP.Integration.Tests.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace SystemyWP.Integration.Tests
{
    public class AppInstance : WebApplicationFactory<Startup>
    {
        public WebApplicationFactory<Startup> AuthenticatedInstance()
        {
            return WithWebHostBuilder(builder =>
            {
                base.ConfigureWebHost(builder);
                builder.ConfigureServices(services =>
                {
                    DbContextUtils.SeedDatabase(services.BuildServiceProvider());
                });
            });
        }
    }
}