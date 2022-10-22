using MasterService.API;
using MasterService.API.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MasterService.Tests.Utilities;

internal class DummyMasterApplication : WebApplicationFactory<Program>
{
    private readonly string _environment;

    public DummyMasterApplication(string environment = "Development") => _environment = environment;

    protected override IHost CreateHost(IHostBuilder builder)
    {
        _ = builder.UseEnvironment(_environment);

        _ = builder.ConfigureServices(services =>
        {
            _ = services.AddScoped(sp =>
            {
                return new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase("master-in-memory-test-db")
                    .UseApplicationServiceProvider(sp)
                    .Options;
            });

            //services.AddHttpClient<GastronomyHttpClient>(_ => new DummyGastronomyApplication().CreateClient());
        });

        return base.CreateHost(builder);
    }
}