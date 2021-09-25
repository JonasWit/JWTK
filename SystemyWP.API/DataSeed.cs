using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using SystemyWP.Data;
using SystemyWP.Data.Enums;
using SystemyWP.Data.Models.General;
using SystemyWP.Data.Models.LegalAppModels.Access;
using SystemyWP.Data.Models.LegalAppModels.Clients;
using SystemyWP.Data.Models.LegalAppModels.Clients.Cases;
using SystemyWP.Data.Models.LegalAppModels.Contacts;
using SystemyWP.Data.Models.LegalAppModels.Reminders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace SystemyWP.API
{
    public static class DataSeed
    {
        private static string GenerateText(int len)
        {
            var builder = new StringBuilder();
            var text =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

            for (var i = 0; i < 6; i++)
                builder.Append(text);

            return builder.ToString().Substring(0, len);
        }

        public static void ProdAdminSeed(ApiIdentityDbContext identityContext, UserManager<IdentityUser> userManager,
            IConfiguration config)
        {
            if (!identityContext.Users.Any(x => x.UserName.Equals("MarzenaWitek")))
            {
                var admin = new IdentityUser("MarAdm") { Email = "marzena.witek@systemywp.pl", EmailConfirmed = true };
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

        public static void DevDataSeedLegalApp(AppDbContext context, string userId, LegalAccessKey legalAccessKey)
        {
            var random = new Random();

            // Seed Clients
            for (var clientNumber = 0; clientNumber < 3; clientNumber++)
            {
                var newClient = new LegalAppClient
                {
                    LegalAccessKey = legalAccessKey,
                    Name = $"Test Client - {clientNumber}",
                    Active = true,
                    CreatedBy = "system",
                    UpdatedBy = "system"
                };

                // Seed Client notes
                for (var clientNoteNumber = 1; clientNoteNumber < 5; clientNoteNumber++)
                {
                    newClient.LegalAppClientNotes.Add(new LegalAppClientNote
                    {
                        AuthorId = userId,
                        UpdatedBy = "system",
                        CreatedBy = "system",
                        Title = $"Note Title {clientNoteNumber} Key: {legalAccessKey.Name}",
                        Message = GenerateText(random.Next(50, 1000)),
                        Created = DateTime.UtcNow.AddDays(clientNoteNumber * 5 * -1),
                        Updated = DateTime.UtcNow.AddDays(clientNoteNumber * 5 * -1)
                    });
                }

                // Seed contact details
                for (var contactDetailsNumber = 0; contactDetailsNumber < 2; contactDetailsNumber++)
                {
                    var contact = new LegalAppContactDetail();
                    contact.Comment = $"Comment for Contact {contactDetailsNumber}";
                    contact.Name = $"Contact {clientNumber} -- {contactDetailsNumber} Key: {legalAccessKey.Name}";
                    contact.Title = $"Title {clientNumber} -- {contactDetailsNumber}";
                    contact.CreatedBy = "system";

                    for (var contactEmailNumber = 0; contactEmailNumber < 2; contactEmailNumber++)
                    {
                        contact.Emails.Add(new LegalAppEmailAddress
                        {
                            Comment = $"TEST Email address {contactDetailsNumber}--{contactEmailNumber}",
                            Email = $"{contactDetailsNumber}@{contactEmailNumber}.com",
                            CreatedBy = "system"
                        });
                    }

                    for (var contactPhone = 0; contactPhone < 2; contactPhone++)
                    {
                        contact.PhoneNumbers.Add(new LegalAppPhoneNumber
                        {
                            Comment = $"TEST PhoneNumber address {contactDetailsNumber}--{contactPhone}",
                            Number = $"{contactPhone}-{contactDetailsNumber}-{contactPhone}",
                            CreatedBy = "system"
                        });
                    }

                    for (var contactAddress = 0; contactAddress < 2; contactAddress++)
                    {
                        contact.PhysicalAddresses.Add(new LegalAppPhysicalAddress
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
                    financeRecord.LawyerName = "Roman Giertch";
                    financeRecord.Amount = random.Next(0, 1000);
                    financeRecord.Rate = random.Next(15, 500);
                    financeRecord.Hours = random.Next(0, 500);
                    financeRecord.Minutes = random.Next(1, 59);
                    financeRecord.Vat = random.Next(0, 23);

                    financeRecord.Name = $"TEST -- {workflowNumber} Key: {legalAccessKey.Name}";
                    financeRecord.EventDate = DateTime.UtcNow.AddDays(workflowNumber * -1);

                    financeRecord.CreatedBy = "system";
                    financeRecord.UserId = userId;

                    newClient.LegalAppClientWorkRecords.Add(financeRecord);
                }

                for (var groupNumber = 0; groupNumber < 3; groupNumber++)
                {
                    for (var caseNumber = 0; caseNumber < 3; caseNumber++)
                    {
                        var newCase = new LegalAppCase
                        {
                            Name = $"Test Case - {caseNumber} - Key: {legalAccessKey.Name}",
                            Signature = $"XYZ-{caseNumber}-XYZ-{caseNumber}",
                            Group = $"Group number {groupNumber}",
                            Description = GenerateText(random.Next(50, 1000)),
                            CreatedBy = "system",
                            UpdatedBy = "system"
                        };

                        for (var i = -1; i < 4; i++)
                        {
                            newCase.LegalAppCaseDeadlines.Add(new LegalAppCaseDeadline
                            {
                                Message = $"Deadline {i} Key: {legalAccessKey.Name}",
                                CreatedBy = "system",
                                Deadline = DateTime.UtcNow.AddDays(i)
                            });
                        }

                        for (var i = 0; i < 5; i++)
                        {
                            newCase.LegalAppCaseNotes.Add(new LegalAppCaseNote
                            {
                                Title = $"Note {i} Key: {legalAccessKey.Name}",
                                Message = GenerateText(random.Next(50, 1000)),
                                UpdatedBy = "system",
                                CreatedBy = "system",
                            });
                        }

                        newClient.LegalAppCases.Add(newCase);
                    }
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

            var random = new Random();

            var databaseCreator =
                (RelationalDatabaseCreator)context.Database.GetService<IDatabaseCreator>();
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

            //User with no key
            identityUser = new IdentityUser($"client-nokey")
            {
                Email = $"client-nokey@test.com",
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
                context.LegalAppAccessKeys.Add(key);
                context.SaveChanges();

                var accKey = context.LegalAppAccessKeys.FirstOrDefault(x => x.Id == key.Id);

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

                //Seed Reminders
                // for (var i = -2; i < 4; i++)
                // {
                //     context.Add(new LegalAppReminder
                //     {
                //         ReminderCategory = ReminderCategory.Memo,
                //         AuthorId = testClientAdmin.Id,
                //         Name = $"Test reminder from {testClientAdmin.Email} for Key: {accKey?.Name}",
                //         LegalAccessKey = accKey,
                //         UpdatedBy = "system",
                //         CreatedBy = "system",
                //         Message = $"Generated message",
                //         Start = DateTime.UtcNow.AddDays(i),
                //         End = DateTime.UtcNow.AddDays(i + random.Next(1, 10)),
                //     });
                // }

                //Seed Clients
                // for (var userNumber = 0; userNumber < 3; userNumber++)
                // {
                //     var testClient = new IdentityUser($"client{userNumber}{adminNumber}")
                //     {
                //         Email = $"client{userNumber}{adminNumber}@test.com",
                //         LockoutEnabled = true,
                //         EmailConfirmed = true
                //     };
                //     userManager.CreateAsync(testClient, "password").GetAwaiter().GetResult();
                //     userManager
                //         .AddClaimsAsync(testClient, new[]
                //         {
                //             SystemyWpConstants.Claims.UserClaim,
                //             SystemyWpConstants.Claims.LegalAppAccessClaim
                //         })
                //         .GetAwaiter()
                //         .GetResult();
                //
                //     context.Add(new User
                //     {
                //         Username = testClient.UserName,
                //         Email = testClient.Email,
                //         LegalAccessKey = accKey,
                //         Id = testClient.Id,
                //         CreatedBy = "system"
                //     });
                //
                //     //Seed Reminders
                //     for (var i = -2; i < 2; i++)
                //     {
                //         context.Add(new LegalAppReminder
                //         {
                //             AuthorId = testClient.Id,
                //             Name = $"Test reminder from {testClient.Email} for Key: {accKey?.Name}",
                //             LegalAccessKey = accKey,
                //             UpdatedBy = "system",
                //             CreatedBy = "system",
                //             Message = $"Generated message",
                //             Start = DateTime.UtcNow.AddDays(i),
                //             End = DateTime.UtcNow.AddDays(i + random.Next(1, 10)),
                //         });
                //     }
                // }

                //DevDataSeedLegalApp(context, testClientAdmin.Id, key);
                context.SaveChanges();
            }
        }
    }
}