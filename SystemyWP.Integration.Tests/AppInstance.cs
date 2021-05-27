using SystemyWP.API;
using SystemyWP.Data;
using SystemyWP.Integration.Tests.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
                    services.RemoveAll(typeof(DbContext));
                    services.AddDbContext<AppDbContext>(options => { options.UseInMemoryDatabase("TestDb"); });  
                    services.AddDbContext<ApiIdentityDbContext>(options => { options.UseInMemoryDatabase("TestDb"); });  
                    DbContextUtils.SeedDatabase(services.BuildServiceProvider());
                });
            });
        }
    }
}