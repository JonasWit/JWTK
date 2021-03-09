using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using SystemyWP.API.Services.PortalLoggerService;
using SystemyWP.Data;
using SystemyWP.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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

            var fakeCounter = 10;

            for (int i = 0; i < 4; i++)
            {
                context.AccessKeys.Add(new AccessKey
                {
                    Name = $"access-key-{i}",
                    ExpireDate = DateTime.UtcNow.AddDays(i),
                    Created = DateTime.UtcNow
                });
            }
            
            context.SaveChanges();
            
            if (!identityContext.Users.Any(x => x.UserName.Equals("test")) && env.IsDevelopment())
            {
                var testClient = new IdentityUser("TestClient")
                {
                    Email = "testClient@test.com",
                    LockoutEnabled = true
                };
                userManager.CreateAsync(testClient, "password").GetAwaiter().GetResult();
                userManager
                    .AddClaimsAsync(testClient, new[]
                    {
                        SystemyWPConstants.Claims.ClientClaim,
                        SystemyWPConstants.Claims.LegalAppAccessClaim
                    })
                    .GetAwaiter()
                    .GetResult();

                context.Add(new User
                {
                    Id = testClient.Id,
                });

                var testClient2 = new IdentityUser("TestClient2")
                {
                    Email = "testClient2@test.com",
                    LockoutEnabled = true
                };
                userManager.CreateAsync(testClient2, "password").GetAwaiter().GetResult();
                userManager
                    .AddClaimsAsync(testClient2, new[]
                    {
                        SystemyWPConstants.Claims.ClientClaim,
                    })
                    .GetAwaiter()
                    .GetResult();

                context.Add(new User
                {
                    Id = testClient2.Id,
                    AccessKey = context.AccessKeys.FirstOrDefault(x => x.Name.Equals("access-key-1"))
                });
                
                var clientAdmin = new IdentityUser("TestClientAdmin")
                {
                    Email = "testAdminClient@test.com", 
                    LockoutEnabled = true
                };
                userManager.CreateAsync(clientAdmin, "password").GetAwaiter().GetResult();
                userManager
                    .AddClaimsAsync(clientAdmin, new[]
                    {
                        SystemyWPConstants.Claims.ClientAdminClaim,
                    })
                    .GetAwaiter()
                    .GetResult();               
                
                context.Add(new User
                {
                    Id = clientAdmin.Id,
                    AccessKey = context.AccessKeys.FirstOrDefault(x => x.Name.Equals("access-key-1"))
                });
                
                var clientAdmin2 = new IdentityUser("TestClientAdmin2")
                {
                    Email = "testAdminClient2@test.com", 
                    LockoutEnabled = true
                };
                userManager.CreateAsync(clientAdmin2, "password").GetAwaiter().GetResult();
                userManager
                    .AddClaimsAsync(clientAdmin2, new[]
                    {
                        SystemyWPConstants.Claims.ClientAdminClaim,
                        SystemyWPConstants.Claims.LegalAppAccessClaim
                    })
                    .GetAwaiter()
                    .GetResult();               
                
                context.Add(new User
                {
                    Id = clientAdmin2.Id,
                    AccessKey = context.AccessKeys.FirstOrDefault(x => x.Name.Equals("access-key-2"))
                });

                var portalAdmin = new IdentityUser("MarzenaWitek") {Email = "testAdminPortal@test.com"};
                userManager.CreateAsync(portalAdmin, "password").GetAwaiter().GetResult();
                userManager
                    .AddClaimsAsync(portalAdmin, new[]
                    {
                        SystemyWPConstants.Claims.LegalAppAccessClaim,
                        SystemyWPConstants.Claims.PortalAdminClaim
                    })
                    .GetAwaiter()
                    .GetResult();               
                
                context.Add(new User
                {
                    Id = portalAdmin.Id,
                });  
                
                context.SaveChanges();


                
                
                
                
                
                
                
            }
            else if (!identityContext.Users
                .Any(x => 
                    x.UserName.Equals("admin")) && env.IsProduction())
            {
                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                var admin = new IdentityUser("admin") {Email = "admin@test.com"};
                userManager
                    .CreateAsync(admin, config
                        .GetSection("AdminPassword").Value)
                    .GetAwaiter()
                    .GetResult();
                userManager
                    .AddClaimAsync(admin, new Claim(SystemyWPConstants.Claims.Role,
                        SystemyWPConstants.Roles.PortalAdmin))
                    .GetAwaiter()
                    .GetResult();
            }
        }
    }
}