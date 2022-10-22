using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MasterService.API.Gastronomy.Data;

namespace MasterService.API.Gastronomy.Tests.Utilities;

internal class DummyGastronomyApplication : WebApplicationFactory<Program>
{
    private readonly string _environment;

    public DummyGastronomyApplication(string environment = "Development")
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
                    .UseInMemoryDatabase("gastronomy-in-memory-test-db")
                    .UseApplicationServiceProvider(sp)
                    .Options;
            });
        });

        return base.CreateHost(builder);
    }
}