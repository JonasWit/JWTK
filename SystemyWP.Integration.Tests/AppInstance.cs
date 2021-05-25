using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using SystemyWP.API;
using SystemyWP.Integration.Tests.Infrastructure;
using SystemyWP.Integration.Tests.Mocks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SystemyWP.Integration.Tests
{
    public class AppInstance : WebApplicationFactory<Startup>
    {
        public WebApplicationFactory<Startup> AuthenticatedInstance()
        {
            return WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton<IAuthenticationSchemeProvider, MockSchemeProvider>();
                    DbContextUtils.SeedDatabase(services.BuildServiceProvider());
                });
            });
        }
    }
    
    public class MockSchemeProvider : AuthenticationSchemeProvider
    {
        public MockSchemeProvider(IOptions<AuthenticationOptions> options) : base(options)
        {
        }

        protected MockSchemeProvider(IOptions<AuthenticationOptions> options, IDictionary<string, AuthenticationScheme> schemes) : base(options, schemes)
        {
        }

        public override Task<AuthenticationScheme> GetDefaultAuthenticateSchemeAsync()
        {
            var mockScheme = new AuthenticationScheme(
                SystemyWpConstants.Roles.ClientAdmin, 
                SystemyWpConstants.Roles.ClientAdmin, 
                typeof(ClientAdminMock));
            return Task.FromResult(mockScheme);
        }
    }
    
}