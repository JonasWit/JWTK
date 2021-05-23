using SystemyWP.Integration.Tests.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Testing;
using SystemyWP.API;
using SystemyWP.Data;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.Integration.Tests.Fixtures
{
    public class SystemyWPFixture : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureServices(services =>
            {
                // services.AddAuthentication()
                //     .AddScheme<AuthenticationSchemeOptions, ClientAdminAuthMock>(TestConstants.AuthenticationSchemes.ClientAdminTest, _ => { })
                //     .AddScheme<AuthenticationSchemeOptions, PortalAdminAuthMock>(TestConstants.AuthenticationSchemes.PortalAdminTest, _ => { })
                //     .AddScheme<AuthenticationSchemeOptions, UnauthorizedAuthMock>(TestConstants.AuthenticationSchemes.UnauthorizedTest, _ => { });
                
                services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("Dev"));
                
                services.AddControllers()
                    .AddFluentValidation(x =>
                        x.RegisterValidatorsFromAssembly(typeof(Startup).Assembly));
                
                DbContextUtils.SeedDatabase(services.BuildServiceProvider());
            });
        }
    }
}