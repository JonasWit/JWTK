using System;
using System.Linq;
using System.Security.Claims;
using SystemyWP.Data;
using SystemyWP.Data.Models.General;
using SystemyWP.Data.Models.LegalAppModels.Clients;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
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

            if (env.IsDevelopment())
            {
                DevIdentitySeed(host);
                DevDataSeedLegalApp(host);
            }
            else if (env.IsProduction())
            {
                ProdAdminSeed(host);
            }
            
            host.Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }

        private static void ProdAdminSeed(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var identityContext = scope.ServiceProvider.GetRequiredService<ApiIdentityDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            
            if (!identityContext.Users.Any(x => x.UserName.Equals("MarzenaWitek")))
            {
                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                var admin = new IdentityUser("admin") {Email = "marzena.witekk@gmail.com"};
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
        
        private static void DevDataSeedLegalApp(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            for (int i = 0; i < 20; i++)
            {
                context.Add(new LegalAppProtectedDataClient
                {
                    DataAccessKey = "access-key-1",
                    Name = $"#{i}# Test Client",
                    Address = $"#{i}# Test Address {i}{i}{i}{i}-{i}{i}{i}{i}/{i}{i}{i}",
                    Active = true,
                    Email = $"test-{i}@email{i}.com",
                    PhoneNumber = $"+{i}{i}-{i}{i}{i}-{i}{i}{i}-{i}{i}{i}"
                });
            }
            
            for (int i = 0; i < 5; i++)
            {
                context.Add(new LegalAppProtectedDataClient
                {
                    DataAccessKey = "access-key-2",
                    Name = $"#{i}# Test Client",
                    Address = $"#{i}# Test Address 1212-2323 / 123123",
                    Active = true,
                    Email = $"test-{i}@email{i}.com",
                    PhoneNumber = $"+{i}{i}-{i}{i}{i}-{i}{i}{i}-{i}{i}{i}"
                });
            }
            context.SaveChanges();
        }

        private static void DevIdentitySeed(IHost host)
        {
            using var scope = host.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var identityContext = scope.ServiceProvider.GetRequiredService<ApiIdentityDbContext>();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if (env.IsDevelopment())
            {
                identityContext.Database.EnsureDeleted();
                identityContext.Database.EnsureCreated();

                var databaseCreator =
                    (RelationalDatabaseCreator) context.Database.GetService<IDatabaseCreator>();
                databaseCreator.CreateTables();
            }

            //Seed access keys
            for (int i = 1; i < 3; i++)
            {
                context.AccessKeys.Add(new AccessKey
                {
                    Name = $"access-key-{i}",
                    ExpireDate = DateTime.UtcNow.AddDays(i),
                    Created = DateTime.UtcNow
                });
            }
            
            //Seed Client Admins
            for (int i = 0; i < 2; i++)
            {
                var clientAdmin = new IdentityUser($"clientadmin{i}")
                {
                    Email = $"clientadmin{i}@test.com",
                    LockoutEnabled = true
                };
                userManager.CreateAsync(clientAdmin, "password").GetAwaiter().GetResult();
                userManager
                    .AddClaimsAsync(clientAdmin, new[]
                    {
                        SystemyWPConstants.Claims.ClientAdminClaim,
                        SystemyWPConstants.Claims.LegalAppAccessClaim
                    })
                    .GetAwaiter()
                    .GetResult();

                context.Add(new User
                {
                    Id = clientAdmin.Id,
                });
            }

            //Seed Clients
            for (int i = 0; i < 10; i++)
            {
                var testClient = new IdentityUser($"client{i}")
                {
                    Email = $"client{i}@test.com",
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
            }
            
            //Seed Poeral Admins
            for (int i = 0; i < 2; i++)
            {
                var portalAdmin = new IdentityUser($"portaladmin{i}")
                {
                    Email = $"portaladmin{i}@test.com"
                };
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
            }
            
            context.SaveChanges();
        }
    }
}