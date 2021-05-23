using SystemyWP.Integration.Tests.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Testing;
using SystemyWP.API;
using FluentValidation.AspNetCore;

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
                
                services.AddControllers()
                    .AddFluentValidation(x =>
                        x.RegisterValidatorsFromAssembly(typeof(Startup).Assembly));
                
                DbContextUtils.SeedDatabase(services.BuildServiceProvider());
            });
        }
    }
}