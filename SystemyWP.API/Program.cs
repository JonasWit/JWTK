using SystemyWP.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SystemyWP.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var identityContext = scope.ServiceProvider.GetRequiredService<ApiIdentityDbContext>();

            if (env.IsDevelopment())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                DataSeed.DevIdentitySeed(context, identityContext, userManager);
                DataSeed.DevDataSeedLegalApp(context, userManager);
            }
            else if (env.IsProduction())
            {
                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                DataSeed.ProdAdminSeed(identityContext, userManager, config);
            }

            host.Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}