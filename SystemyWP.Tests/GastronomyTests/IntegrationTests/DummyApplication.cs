using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SystemyWP.API.Gastronomy.Data;

namespace SystemyWP.Tests.GastronomyTests.IntegrationTests;

public class MockedGastronomyApplication: WebApplicationFactory<Program>
{
    private readonly string _environment;
    
    public MockedGastronomyApplication(string environment = "Development")
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
                // Replace SQLite with in-memory database for tests
                return new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase("gastronomy-in-memory-test-db")
                    .UseApplicationServiceProvider(sp)
                    .Options;
            });
        });

        return base.CreateHost(builder);
    }
}