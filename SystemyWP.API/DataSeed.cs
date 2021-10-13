using System;
using System.Linq;
using SystemyWP.Data;
using SystemyWP.Data.Models.General;
using SystemyWP.Data.Models.LegalAppModels.Access;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace SystemyWP.API
{
    public static class DataSeed
    {
        public static void ProdAdminSeed(ApiIdentityDbContext identityContext, UserManager<IdentityUser> userManager,
            IConfiguration config)
        {
            if (!identityContext.Users.Any(x => x.UserName.Equals("MarAdm")))
            {
                var admin = new IdentityUser("MarAdm") { Email = "marzena.witek@systemywp.pl", EmailConfirmed = true };
                userManager.CreateAsync(admin, config.GetSection("AdminPassword").Value).GetAwaiter().GetResult();
                userManager.AddClaimsAsync(admin, new[]
                    {
                        SystemyWpConstants.Claims.LegalAppAccessClaim,
                        SystemyWpConstants.Claims.PortalAdminClaim
                    })
                    .GetAwaiter()
                    .GetResult();
            }
        }

        public static void DevSeed(AppDbContext context, ApiIdentityDbContext identityContext,
            UserManager<IdentityUser> userManager)
        {
            if (identityContext.Users.Count(x => x.UserName.Contains("user")) < 10)
            {
                for (var i = 0; i < 15; i++)
                {
                    var identityUser = new IdentityUser($"user{i}")
                    {
                        Email = $"user{i}@test.com",
                        LockoutEnabled = true,
                        EmailConfirmed = true
                    };
                    userManager.CreateAsync(identityUser, "password").GetAwaiter().GetResult();
                    userManager
                        .AddClaimsAsync(identityUser, new[]
                        {
                            SystemyWpConstants.Claims.UserClaim,
                            SystemyWpConstants.Claims.LegalAppAccessClaim
                        })
                        .GetAwaiter()
                        .GetResult();

                    context.Add(new User
                    {
                        Username = identityUser.UserName,
                        Email = identityUser.Email,
                        Id = identityUser.Id,
                        CreatedBy = "system"
                    });
                }           
            }
            
            if (!identityContext.Users.Any(x => x.UserName.Equals("portaladmin1")))
            {
                //Seed Portal Admins
                for (var i = 1; i < 2; i++)
                {
                    var testPortalAdmin = new IdentityUser($"portaladmin{i}")
                    {
                        Email = $"portaladmin{i}@test.com",
                        EmailConfirmed = true
                    };
                    userManager.CreateAsync(testPortalAdmin, "password").GetAwaiter().GetResult();
                    userManager
                        .AddClaimsAsync(testPortalAdmin, new[]
                        {
                            SystemyWpConstants.Claims.LegalAppAccessClaim,
                            SystemyWpConstants.Claims.PortalAdminClaim
                        })
                        .GetAwaiter()
                        .GetResult();

                    context.Add(new User
                    {
                        Username = testPortalAdmin.UserName,
                        Email = testPortalAdmin.Email,
                        Id = testPortalAdmin.Id,
                        CreatedBy = "system"
                    });
                }

                //Admin User with no key
                var identityUser = new IdentityUser($"clientadmin-nokey")
                {
                    Email = $"clientadmin-nokey@test.com",
                    LockoutEnabled = true,
                    EmailConfirmed = true
                };
                userManager.CreateAsync(identityUser, "password").GetAwaiter().GetResult();
                userManager
                    .AddClaimsAsync(identityUser, new[]
                    {
                        SystemyWpConstants.Claims.UserAdminClaim,
                        SystemyWpConstants.Claims.LegalAppAccessClaim
                    })
                    .GetAwaiter()
                    .GetResult();

                context.Add(new User
                {
                    Username = identityUser.UserName,
                    Email = identityUser.Email,
                    Id = identityUser.Id,
                    CreatedBy = "system"
                });
                
                //Seed Client Admins
                for (var adminNumber = -2; adminNumber < 3; adminNumber++)
                {
                    var key = new LegalAccessKey
                    {
                        Name = $"access-key for {adminNumber}",
                        ExpireDate = DateTime.UtcNow.AddDays(adminNumber),
                        Created = DateTime.UtcNow,
                        CreatedBy = "system",
                        UpdatedBy = "system",
                    };
                    context.LegalAccessKeys.Add(key);
                    context.SaveChanges();

                    var accKey = context.LegalAccessKeys.FirstOrDefault(x => x.Id == key.Id);

                    var testClientAdmin = new IdentityUser($"clientadmin{adminNumber}")
                    {
                        Email = $"clientadmin{adminNumber}@test.com",
                        LockoutEnabled = true,
                        EmailConfirmed = true
                    };

                    userManager.CreateAsync(testClientAdmin, "password").GetAwaiter().GetResult();
                    userManager
                        .AddClaimsAsync(testClientAdmin, new[]
                        {
                            SystemyWpConstants.Claims.UserAdminClaim,
                            SystemyWpConstants.Claims.LegalAppAccessClaim
                        })
                        .GetAwaiter()
                        .GetResult();

                    context.Add(new User
                    {
                        Username = testClientAdmin.UserName,
                        Email = testClientAdmin.Email,
                        LegalAccessKey = accKey,
                        Id = testClientAdmin.Id,
                        CreatedBy = "system"
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}