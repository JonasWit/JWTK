using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SystemyWP.API.Gastronomy.Data;
using SystemyWP.API.Services.HttpServices;

namespace SystemyWP.API.Tests.Utilities;

internal class DummyMasterApplication : WebApplicationFactory<Program>
{
    private readonly string _environment;

    public DummyMasterApplication(string environment = "Test")
    {
        _environment = environment;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment(_environment);

        builder.ConfigureServices(services =>
        {
            services.AddScoped(sp =>
            {
                return new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase("master-in-memory-test-db")
                    .UseApplicationServiceProvider(sp)
                    .Options;
            });

            services.AddHttpClient("TEST", config =>
            {
                
            });
            
            services.AddHttpClient<GastronomyHttpClient>();



        });

        return base.CreateHost(builder);
    }
}