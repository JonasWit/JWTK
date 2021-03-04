using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using SystemyWP.Data;
using SystemyWP.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
            DataSeed(host);
            host.Run();
        }
        
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        } 

        private static void DataSeed(IHost host)
        {
            using var scope = host.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var identityContext = scope.ServiceProvider.GetRequiredService<ApiIdentityDbContext>();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if (!identityContext.Users.Any(x => x.UserName.Equals("test")) && env.IsDevelopment())
            {
                var testClient = new IdentityUser("TestClient") {Email = "testClient@test.com"};
                userManager.CreateAsync(testClient, "password").GetAwaiter().GetResult();
                userManager
                    .AddClaimsAsync(testClient, new[]
                    {
                        new Claim(SystemyWPConstants.Claims.Role, SystemyWPConstants.Roles.Client),
                        new Claim(SystemyWPConstants.Claims.DataAccessKey, "profile-test-1"),
                        new Claim(SystemyWPConstants.Claims.AppAccess,
                            SystemyWPConstants.Apps.LegalApp)
                    })
                    .GetAwaiter()
                    .GetResult();

                context.Add(new User
                {
                    Id = testClient.Id,
                });
                
                var clientAdmin = new IdentityUser("TestClientAdmin") {Email = "testAdminClient@test.com"};
                userManager.CreateAsync(clientAdmin, "password").GetAwaiter().GetResult();
                userManager
                    .AddClaimAsync(clientAdmin, new Claim(SystemyWPConstants.Claims.Role,
                        SystemyWPConstants.Roles.ClientAdmin))
                    .GetAwaiter()
                    .GetResult();               
                
                context.Add(new User
                {
                    Id = clientAdmin.Id,
                });

                var portalAdmin = new IdentityUser("TestPortalAdmin") {Email = "testAdminPortal@test.com"};
                userManager.CreateAsync(portalAdmin, "password").GetAwaiter().GetResult();
                userManager
                    .AddClaimsAsync(portalAdmin, new[]
                    {
                        new Claim(SystemyWPConstants.Claims.Role, SystemyWPConstants.Roles.PortalAdmin),
                        new Claim(SystemyWPConstants.Claims.DataAccessKey, "portalAdmin-key"),
                        new Claim(SystemyWPConstants.Claims.AppAccess, SystemyWPConstants.Apps.LegalApp)
                    })
                    .GetAwaiter()
                    .GetResult();               
                
                context.Add(new User
                {
                    Id = portalAdmin.Id,
                });  
        
                
                context.SaveChanges();
            }
            else if (!identityContext.Users.Any(x => x.UserName.Equals("admin")) && env.IsProduction())
            {
                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                var admin = new IdentityUser("admin") {Email = "admin@test.com"};
                userManager.CreateAsync(admin, config.GetSection("AdminPassword").Value).GetAwaiter().GetResult();
                userManager
                    .AddClaimAsync(admin, new Claim(SystemyWPConstants.Claims.Role,
                        SystemyWPConstants.Roles.PortalAdmin))
                    .GetAwaiter()
                    .GetResult();
            }
        }
    }
}