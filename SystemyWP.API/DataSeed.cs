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

        public static void DevDataSeedLegalApp(AppDbContext context, string userId, AccessKey accessKey)
        {
            var random = new Random();

            for (var clientNumber = 0; clientNumber < 100; clientNumber++)
            {
                var newClient = new LegalAppClient
                {
                    AccessKey = accessKey,
                    Name = $"Test Client - {clientNumber}",
                    Active = true,
                    CreatedBy = "system",
                    UpdatedBy = "system"
                };

                for (int clientNoteNumber = 0; clientNoteNumber < 50; clientNoteNumber++)
                {
                    newClient.LegalAppClientNotes.Add(new LegalAppClientNote
                    {
                        CreatedBy = "system",
                        Title = $"Note Title {clientNoteNumber}",
                        Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
                    });
                }

                // Seed contact details
                for (var contactDetailsNumber = 0; contactDetailsNumber < 20; contactDetailsNumber++)
                {
                    var contact = new ContactDetails();
                    contact.Comment = $"Comment for Contact {contactDetailsNumber}";
                    contact.Name = $"Contact {clientNumber} -- {contactDetailsNumber}";
                    contact.Title = $"Title {clientNumber} -- {contactDetailsNumber}";
                    contact.CreatedBy = "system";

                    for (var contactEmailNumber = 0; contactEmailNumber < 20; contactEmailNumber++)
                    {
                        contact.Emails.Add(new EmailAddress
                        {
                            Comment = $"TEST Email address {contactDetailsNumber}--{contactEmailNumber}",
                            Email = $"{contactDetailsNumber}@{contactEmailNumber}.com",
                            CreatedBy = "system"
                        });
                    }

                    for (var contactPhone = 0; contactPhone < 10; contactPhone++)
                    {
                        contact.PhoneNumbers.Add(new PhoneNumber
                        {
                            Comment = $"TEST PhoneNumber address {contactDetailsNumber}--{contactPhone}",
                            Number = $"{contactPhone}-{contactDetailsNumber}-{contactPhone}",
                            CreatedBy = "system"
                        });
                    }

                    for (var contactAddress = 0; contactAddress < 10; contactAddress++)
                    {
                        contact.PhysicalAddresses.Add(new PhysicalAddress
                        {
                            Street = $"Street - Physical address - {contactAddress}",
                            Comment = $"TEST Physical address - {contactAddress}",
                            CreatedBy = "system"
                        });
                    }

                    newClient.Contacts.Add(contact);
                }

                // Seed workflow
                for (var workflowNumber = 0; workflowNumber < 30; workflowNumber++)
                {
                    var financeRecord = new LegalAppClientWorkRecord();
                    financeRecord.Amount = random.Next(0, 1000);
                    financeRecord.Rate = random.Next(15, 500);
                    financeRecord.Hours = random.Next(0, 500);
                    financeRecord.Minutes = random.Next(1, 59);
                    financeRecord.Vat = random.Next(0, 23);

                    financeRecord.Name = $"TEST -- {workflowNumber}";
                    financeRecord.EventDate = DateTime.UtcNow.AddDays(workflowNumber * -1);

                    financeRecord.CreatedBy = "system";
                    financeRecord.UserId = userId;
                    financeRecord.UserEmail = "test@test.pl";

                    newClient.LegalAppClientWorkRecords.Add(financeRecord);
                }

                for (var caseNumber = 0; caseNumber < 60; caseNumber++)
                {
                    newClient.LegalAppCases.Add(new LegalAppCase
                    {
                        Name = $"Test Case - {caseNumber} - Lorem ipsum dolor sit amet",
                        Signature = $"XYZ-{caseNumber}-XYZ-{caseNumber}",
                        Description =
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                        CreatedBy = "system",
                        UpdatedBy = "system"
                    });
                }

                newClient.CreatedBy = "system";
                context.Add(newClient);
            }
        }

        public static void DevSeed(AppDbContext context, ApiIdentityDbContext identityContext,
            UserManager<IdentityUser> userManager)
        {
            identityContext.Database.EnsureDeleted();
            identityContext.Database.EnsureCreated();

            var databaseCreator =
                (RelationalDatabaseCreator) context.Database.GetService<IDatabaseCreator>();
            databaseCreator.CreateTables();

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
                    Id = testPortalAdmin.Id,
                    CreatedBy = "system"
                });
            }

            //Seed Client Admins
            for (var adminNumber = -2; adminNumber < 3; adminNumber++)
            {
                var key = new AccessKey
                {
                    Name = $"access-key for {adminNumber}",
                    ExpireDate = DateTime.UtcNow.AddDays(adminNumber),
                    Created = DateTime.UtcNow,
                    CreatedBy = "system",
                    UpdatedBy = "system",
                };
                context.AccessKeys.Add(key);
                context.SaveChanges();

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
                        SystemyWpConstants.Claims.ClientAdminClaim,
                        SystemyWpConstants.Claims.LegalAppAccessClaim
                    })
                    .GetAwaiter()
                    .GetResult();

                context.Add(new User
                {
                    AccessKey = context.AccessKeys.FirstOrDefault(x => x.Id == key.Id),
                    Id = testClientAdmin.Id,
                    CreatedBy = "system"
                });

                //Seed Clients
                for (var userNumber = 0; userNumber < 6; userNumber++)
                {
                    var testClient = new IdentityUser($"client{userNumber}{adminNumber}")
                    {
                        Email = $"client{userNumber}{adminNumber}@test.com",
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

                    context.Add(new User
                    {
                        AccessKey = context.AccessKeys.FirstOrDefault(x => x.Id == key.Id),
                        Id = testClient.Id,
                        CreatedBy = "system"
                    });
                }

                DevDataSeedLegalApp(context, testClientAdmin.Id, key);
                context.SaveChanges();
            }
        }
    }
}