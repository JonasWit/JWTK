using System;
using System.Linq;
using System.Security.Claims;
using SystemyWP.Data;
using SystemyWP.Data.Models.General;
using SystemyWP.Data.Models.General.Contact;
using SystemyWP.Data.Models.LegalAppModels.Cases;
using SystemyWP.Data.Models.LegalAppModels.Clients;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace SystemyWP.API
{
    public static class DataSeed
    {
        public static void ProdAdminSeed(ApiIdentityDbContext identityContext, UserManager<IdentityUser> userManager,
            IConfiguration config)
        {
            if (!identityContext.Users.Any(x => x.UserName.Equals("MarzenaWitek")))
            {
                var admin = new IdentityUser("admin") {Email = "marzena.witekk@gmail.com"};
                userManager
                    .CreateAsync(admin, config
                        .GetSection("AdminPassword").Value)
                    .GetAwaiter()
                    .GetResult();
                userManager
                    .AddClaimAsync(admin, new Claim(SystemyWpConstants.Claims.Role,
                        SystemyWpConstants.Roles.PortalAdmin))
                    .GetAwaiter()
                    .GetResult();
            }
        }
        
        public static void DevDataSeedLegalApp(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            var random = new Random();

            var adminUsers = userManager
                .GetUsersForClaimAsync(SystemyWpConstants.Claims.ClientAdminClaim)
                .GetAwaiter()
                .GetResult();
            var profiles = context.Users
                .Include(x => x.AccessKey)
                .AsEnumerable()
                .Where(x => adminUsers.Any(y => y.Id == x.Id) && x.AccessKey != null)
                .ToList();

            for (var i = 0; i < 500; i++)
            {
                var newClient = new LegalAppClient
                {
                    Name = $"#{i}# Test Client",
                    Active = true,
                    CreatedBy = "portaladmin1",
                    UpdatedBy = "portaladmin1"
                };

                for (var c = 0; c < random.Next(0, 60); c++)
                {
                    var contact = new ContactDetails();
                    contact.Comment = $"Comment for Contact {i} -- {c}";
                    contact.Name = $"Contact {i} -- {c}";
                    contact.Title = $"Title {i} -- {c}";
                    contact.CreatedBy = "system";

                    for (var em = 0; em < random.Next(0, 60); em++)
                    {
                        contact.Emails.Add(new EmailAddress
                        {
                            Comment = $"TEST Email address {c}--{em}",
                            Email = $"{c}@{em}.com",
                            CreatedBy = "system"
                        });
                    }

                    for (var em = 0; em < random.Next(0, 60); em++)
                    {
                        contact.PhoneNumbers.Add(new PhoneNumber
                        {
                            Comment = $"TEST PhoneNumber address {c}--{em}",
                            Number = $"{em}-{c}-{em}",
                            CreatedBy = "system"
                        });
                    }

                    for (var em = 0; em < random.Next(0, 60); em++)
                    {
                        contact.PhysicalAddresses.Add(new PhysicalAddress
                        {
                            Street = $"Street - Physical address - {em}",
                            Comment = $"TEST Physical address - {em}",
                            CreatedBy = "system"
                        });
                    }

                    newClient.Contacts.Add(contact);

                    for (var em = 0; em < random.Next(1, 60); em++)
                    {
                        var financeRecord = new LegalAppClientWorkRecord();
                        financeRecord.Amount = random.Next(0, 1000);
                        financeRecord.Hours = random.Next(0, 500);
                        financeRecord.Minutes = random.Next(1, 59);

                        financeRecord.Name = $"TEST -- {em}";
                        financeRecord.EventDate = DateTime.UtcNow.AddDays(em * -1);

                        financeRecord.CreatedBy = "system";
                        financeRecord.UserId = profiles[random.Next(0, profiles.Count)]?.Id;
                        financeRecord.UserEmail = userManager
                            .FindByIdAsync(financeRecord.UserId)?
                            .GetAwaiter()
                            .GetResult()
                            .Email;

                        newClient.LegalAppClientWorkRecords.Add(financeRecord);
                    }
                }

                if (i % 2 == 0)
                {
                    for (var j = 0; j < random.Next(0, 20); j++)
                    {
                        newClient.LegalAppCases.Add(new LegalAppCase
                        {
                            Name = $"Test Case - {j} - Lorem ipsum dolor sit amet",
                            Signature = $"XYZ-{j}-XYZ-{j}",
                            Description =
                                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                            CreatedBy = "system",
                            UpdatedBy = "system"
                        });
                    }
                }

                if (i < 100)
                {
                    newClient.AccessKey = context.AccessKeys.FirstOrDefault(x => x.Name.Equals("access-key Overdue"));
                    newClient.CreatedBy = "system";
                    context.Add(newClient);
                    continue;
                }

                if (i < 300)
                {
                    newClient.AccessKey = context.AccessKeys.FirstOrDefault(x => x.Name.Equals("access-key Active 1"));
                    newClient.CreatedBy = "system";
                    context.Add(newClient);
                    continue;
                }

                if (i < 500)
                {
                    newClient.AccessKey = context.AccessKeys.FirstOrDefault(x => x.Name.Equals("access-key Active 2"));
                    newClient.CreatedBy = "system";
                    context.Add(newClient);
                }
            }

            context.SaveChanges();
        }

        public static void DevIdentitySeed(AppDbContext context, ApiIdentityDbContext identityContext,
            UserManager<IdentityUser> userManager)
        {
            identityContext.Database.EnsureDeleted();
            identityContext.Database.EnsureCreated();

            var databaseCreator =
                (RelationalDatabaseCreator) context.Database.GetService<IDatabaseCreator>();
            databaseCreator.CreateTables();

            #region Seed Keys

            context.AccessKeys.Add(new AccessKey
            {
                Name = $"access-key Overdue",
                ExpireDate = DateTime.UtcNow.AddDays(-4),
                Created = DateTime.UtcNow,
                CreatedBy = "system",
                UpdatedBy = "system",
            });

            context.AccessKeys.Add(new AccessKey
            {
                Name = $"access-key Active-Empty",
                ExpireDate = DateTime.UtcNow.AddDays(1),
                Created = DateTime.UtcNow,
                CreatedBy = "system",
                UpdatedBy = "system",
            });

            context.AccessKeys.Add(new AccessKey
            {
                Name = $"access-key Active 1",
                ExpireDate = DateTime.UtcNow.AddDays(2),
                Created = DateTime.UtcNow,
                CreatedBy = "system",
                UpdatedBy = "system",
            });

            context.AccessKeys.Add(new AccessKey
            {
                Name = $"access-key Active 2",
                ExpireDate = DateTime.UtcNow.AddDays(3),
                Created = DateTime.UtcNow,
                CreatedBy = "system",
                UpdatedBy = "system",
            });

            context.SaveChanges();

            #endregion

            //Seed Client Admins
            for (var i = 0; i < 8; i++)
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
                        SystemyWpConstants.Claims.ClientAdminClaim,
                        SystemyWpConstants.Claims.LegalAppAccessClaim
                    })
                    .GetAwaiter()
                    .GetResult();

                if (i <= 1)
                {
                    var userRecord = new User
                    {
                        Id = clientAdmin.Id,
                        AccessKey = context.AccessKeys.FirstOrDefault(x => x.Name.Equals("access-key Overdue")),
                        CreatedBy = "system"
                    };
                    context.Add(userRecord);
                }
                else if (i <= 3)
                {
                    var userRecord = new User
                    {
                        Id = clientAdmin.Id,
                        AccessKey = context.AccessKeys.FirstOrDefault(x => x.Name.Equals("access-key Active-Empty")),
                        CreatedBy = "system"
                    };
                    context.Add(userRecord);
                }
                else if (i <= 5)
                {
                    var userRecord = new User
                    {
                        Id = clientAdmin.Id,
                        AccessKey = context.AccessKeys.FirstOrDefault(x => x.Name.Equals("access-key Active 1")),
                        CreatedBy = "system"
                    };
                    context.Add(userRecord);
                }
                else if (i <= 7)
                {
                    var userRecord = new User
                    {
                        Id = clientAdmin.Id,
                        AccessKey = context.AccessKeys.FirstOrDefault(x => x.Name.Equals("access-key Active 2")),
                        CreatedBy = "system"
                    };
                    context.Add(userRecord);
                }
            }

            //Seed Clients
            for (var i = 0; i < 16; i++)
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
                        SystemyWpConstants.Claims.ClientClaim,
                        SystemyWpConstants.Claims.LegalAppAccessClaim
                    })
                    .GetAwaiter()
                    .GetResult();

                if (i <= 3)
                {
                    var userRecord = new User
                    {
                        Id = testClient.Id,
                        AccessKey = context.AccessKeys.FirstOrDefault(x => x.Name.Equals("access-key Overdue")),
                        CreatedBy = "system"
                    };
                    context.Add(userRecord);
                }
                else if (i <= 6)
                {
                    var userRecord = new User
                    {
                        Id = testClient.Id,
                        AccessKey = context.AccessKeys.FirstOrDefault(x => x.Name.Equals("access-key Active-Empty")),
                        CreatedBy = "system"
                    };
                    context.Add(userRecord);
                }
                else if (i <= 9)
                {
                    var userRecord = new User
                    {
                        Id = testClient.Id,
                        AccessKey = context.AccessKeys.FirstOrDefault(x => x.Name.Equals("access-key Active 1")),
                        CreatedBy = "system"
                    };
                    context.Add(userRecord);
                }
                else if (i <= 15)
                {
                    var userRecord = new User
                    {
                        Id = testClient.Id,
                        AccessKey = context.AccessKeys.FirstOrDefault(x => x.Name.Equals("access-key Active 2")),
                        CreatedBy = "system"
                    };
                    context.Add(userRecord);
                }
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
                    SystemyWpConstants.Claims.LegalAppAccessClaim,
                    SystemyWpConstants.Claims.PortalAdminClaim
                })
                .GetAwaiter()
                .GetResult();

            context.Add(new User
            {
                AccessKey = context.AccessKeys.FirstOrDefault(x => x.Name.Equals("access-key Active 2")),
                Id = portalAdmin.Id,
                CreatedBy = "system"
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
                        SystemyWpConstants.Claims.LegalAppAccessClaim,
                        SystemyWpConstants.Claims.PortalAdminClaim
                    })
                    .GetAwaiter()
                    .GetResult();

                context.Add(new User
                {
                    AccessKey = context.AccessKeys.FirstOrDefault(x => x.Name.Equals("access-key Active 2")),
                    Id = portalAdmin.Id,
                    CreatedBy = "system"
                });
            }

            context.SaveChanges();
        }
    }
}