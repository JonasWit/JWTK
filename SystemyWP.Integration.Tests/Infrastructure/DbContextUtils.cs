using System;
using SystemyWP.API;
using SystemyWP.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace SystemyWP.Integration.Tests.Infrastructure
{
    public class DbContextUtils
    {
        public static void SeedDatabase(IServiceProvider provider)
        {
            using (var scope = provider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var identityContext = scope.ServiceProvider.GetRequiredService<ApiIdentityDbContext>();
                
                DataSeed.DevIdentitySeed(context, identityContext, userManager);
                DataSeed.DevDataSeedLegalApp(context, userManager);
            }
        }
    }
}