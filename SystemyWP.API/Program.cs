using System;
using System.Linq;
using System.Security.Claims;
using SystemyWP.Data;
using SystemyWP.Data.Models.General;
using SystemyWP.Data.Models.LegalAppModels.Cases;
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
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var adminUser = userManager.FindByNameAsync("portaladmin1");
            var random = new Random();

            for (var i = 0; i < 50; i++)
            {
                var newClient = new LegalAppClient
                {
                    DataAccessKey = "access-key-1",
                    Name = $"#{i}# Test Client",
                    Address = $"#{i}# Test Address {i}{i}{i}{i}-{i}{i}{i}{i}/{i}{i}{i}",
                    Active = true,
                    Email = $"test-{i}@email{i}.com",
                    PhoneNumber = $"+{i}{i}-{i}{i}{i}-{i}{i}{i}-{i}{i}{i}",
                    CreatedBy = "portaladmin1",
                    UpdatedBy = "portaladmin1"
                };

                if (i % 2 == 0)
                {
                    for (int j = 0; j < random.Next(1, 20); j++)
                    {
                        newClient.LegalAppCases.Add(new LegalAppCase
                        {
                            Name = $"Test Case - {j} - Lorem ipsum dolor sit amet",
                            Signature = $"XYZ-{j}-XYZ-{j}",
                            Description =
                                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                            CreatedBy = "portaladmin1",
                            UpdatedBy = "portaladmin1"
                        });
                    }
                }

                if (i % 2 == 0)
                {
                    newClient.DataAccessKey = "access-key-1";
                    context.Add(newClient);
                }
                else if (i % 3 == 0)
                {
                    newClient.DataAccessKey = "access-key-3";
                    context.Add(newClient);
                }
                else
                {
                    newClient.DataAccessKey = "access-key-2";
                    context.Add(newClient);  
                }
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
            for (var i = -2; i < 6; i++)
            {
                context.AccessKeys.Add(new AccessKey
                {
                    Name = $"access-key-{i}",
                    ExpireDate = DateTime.UtcNow.AddDays(i),
                    Created = DateTime.UtcNow
                });
            }

            context.SaveChanges();

            //Seed Client Admins
            for (var i = 0; i < 7; i++)
            {
                var clientAdmin = new IdentityUser($"clientadmin{i}")
                {
                    Email = $"clientadmin{i}@test.com",
                    LockoutEnabled = true,
                    EmailConfirmed = true
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

                switch (i)
                {
                    case 1:
                    {
                        var userRecord = new User
                        {
                            Id = clientAdmin.Id,
                            AccessKey = context.AccessKeys.FirstOrDefault(x => x.Name.Equals("access-key-4"))
                        };    
                        context.Add(userRecord);
                        break;
                    }
                    case 2:
                    {
                        var userRecord = new User
                        {
                            Id = clientAdmin.Id,
                            AccessKey = context.AccessKeys.FirstOrDefault(x => x.Name.Equals("access-key-5"))
                        };    
                        context.Add(userRecord);
                        break;
                    }
                    default:
                    {
                        var userRecord = new User
                        {
                            Id = clientAdmin.Id,
                            AccessKey = i % 2 == 0
                                ? context.AccessKeys.FirstOrDefault(x => x.Name.Equals("access-key-1"))
                                : context.AccessKeys.FirstOrDefault(x => x.Name.Equals("access-key-2"))
                        };
                        break;
                    }
                }
            }

            //Seed Clients
            for (var i = 0; i < 12; i++)
            {
                var testClient = new IdentityUser($"client{i}")
                {
                    Email = $"client{i}@test.com",
                    LockoutEnabled = true,
                    EmailConfirmed = true
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

                var userRecord = new User {Id = testClient.Id};

                if (i == 1)
                {
                    userRecord.AccessKey = context.AccessKeys.FirstOrDefault(x => x.Name.Equals("access-key-4"));
                }
                
                if (i % 2 == 0)
                {
                    userRecord.AccessKey = context.AccessKeys.FirstOrDefault(x => x.Name.Equals("access-key-1"));
                }
                else if (i % 3 == 0)
                {
                    userRecord.AccessKey = context.AccessKeys.FirstOrDefault(x => x.Name.Equals("access-key-2"));
                }

                context.Add(userRecord);
            }

            //Seed Portal Admins
            
            var portalAdmin = new IdentityUser($"pat")
            {
                Email = $"witek.j87@gmail.com",
                EmailConfirmed = true
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
                AccessKey = context.AccessKeys.FirstOrDefault(x => x.Name.Equals("access-key-1")),
                Id = portalAdmin.Id,
            });
            
            for (var i = 1; i < 2; i++)
            {
                portalAdmin = new IdentityUser($"portaladmin{i}")
                {
                    Email = $"portaladmin{i}@test.com",
                    EmailConfirmed = true
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
                    AccessKey = context.AccessKeys.FirstOrDefault(x => x.Name.Equals("access-key-1")),
                    Id = portalAdmin.Id,
                });
            }

            context.SaveChanges();
        }
    }
}