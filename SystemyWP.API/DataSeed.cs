using System.Linq;
using SystemyWP.Data;
using SystemyWP.Data.Models.General;
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
            if (identityContext.Users.Count() == 0)
            {
                //Seed Portal Admins
                for (var userNo = 0; userNo < 3; userNo++)
                {
                    var identityUser = new IdentityUser($"portaladmin{userNo}")
                    {
                        Email = $"portaladmin{userNo}@test.com",
                        EmailConfirmed = true
                    };
                    userManager.CreateAsync(identityUser, "password").GetAwaiter().GetResult();
                    userManager
                        .AddClaimsAsync(identityUser, new[]
                        {
                            SystemyWpConstants.Claims.PortalAdminClaim
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
                
                //Seed Normal Users
                for (var userNo = 0; userNo < 5; userNo++)
                {
                    var identityUser = new IdentityUser($"user{userNo}")
                    {
                        Email = $"user{userNo}@test.com",
                        LockoutEnabled = true,
                        EmailConfirmed = true
                    };
                    userManager.CreateAsync(identityUser, "password").GetAwaiter().GetResult();
                    userManager
                        .AddClaimsAsync(identityUser, new[]
                        {
                            SystemyWpConstants.Claims.UserClaim,
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
                
                //Seed Client Admins
                for (var userNo = 0; userNo < 10; userNo++)
                {
                    var identityUser = new IdentityUser($"clientadmin{userNo}")
                    {
                        Email = $"clientadmin{userNo}@test.com",
                        LockoutEnabled = true,
                        EmailConfirmed = true
                    };

                    userManager.CreateAsync(identityUser, "password").GetAwaiter().GetResult();
                    userManager
                        .AddClaimsAsync(identityUser, new[]
                        {
                            SystemyWpConstants.Claims.UserAdminClaim,
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
                
                context.SaveChanges();
            }
        }
    }
}