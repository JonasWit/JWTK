﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms;
using SystemyWP.API.Projections;
using SystemyWP.API.Projections.LegalApp.LegalAppAdmin;
using SystemyWP.API.Services.PortalLoggerService;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.LegalApp
{
    [Route("/api/legal-app-admin")]
    [Authorize(SystemyWPConstants.Policies.ClientAdmin)]
    public class LegalAppAdminController : ApiController
    {
        public LegalAppAdminController(PortalLogger portalLogger) : base(portalLogger)
        {
        }

        [HttpGet("related-users")]
        public async Task<ActionResult<IEnumerable<object>>> GetRelatedUsers(
            [FromServices] AppDbContext context,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            var result = new List<object>();

            try
            {
                //Get current admin who made request
                var adminUser = context.Users
                    .Include(x => x.AccessKey)
                    .FirstOrDefault(x => x.Id.Equals(UserId));

                if (adminUser is null) throw new Exception("Error!");

                //Get related users with the same data access key
                var relatedUsers = context.Users
                    .Include(x => x.AccessKey)
                    .Where(x => x.AccessKey.Name.Equals(adminUser.AccessKey.Name))
                    .Include(x => x.DataAccess)
                    .ToList();

                if (relatedUsers.Count == 0) return Ok(result);

                foreach (var relatedUser in relatedUsers)
                {
                    var userRecord = await userManager.FindByIdAsync(relatedUser.Id);
                    var role = (await userManager.GetClaimsAsync(userRecord) as List<Claim>)
                        .FirstOrDefault(x => x.Type.Equals(SystemyWPConstants.Claims.Role))?
                        .Value;
                    result.Add(UserProjections
                        .RelatedUserProjection(userRecord.UserName, userRecord.Email, role)
                        .Compile()
                        .Invoke(relatedUser));
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update-legal-app-data-access")]
        public async Task<IActionResult> UpdateLegalAppDataAccess(
            [FromBody] LegalAppUpdateUserAccessForm form,
            [FromServices] AppDbContext context)
        {
            try
            {
                var requester = context.Users
                    .Include(x => x.AccessKey)
                    .FirstOrDefault(x => x.Id.Equals(UserId));

                var user = context.Users
                    .Include(x => x.AccessKey)
                    .FirstOrDefault(x => x.Id.Equals(form.UserId));

                if (requester is null ||
                    user is null ||
                    requester.AccessKey is null ||
                    user.AccessKey is null)
                {
                    return BadRequest();
                }

                if (!requester.AccessKey.Name.Equals(user.AccessKey.Name))
                {
                    return BadRequest();
                }

                context.DataAccesses.RemoveRange(
                    context.DataAccesses
                        .Where(x => x.UserId.Equals(user.Id)));

                foreach (var allowedClient in form.AllowedClients)
                {
                    context.Add(new DataAccess
                    {
                        RestrictedType = RestrictedType.LegalAppClient,
                        ItemId = allowedClient,
                        UserId = user.Id
                    });
                }

                foreach (var allowedCase in form.AllowedCases)
                {
                    context.Add(new DataAccess
                    {
                        RestrictedType = RestrictedType.LegalAppCase,
                        ItemId = allowedCase,
                        UserId = user.Id,
                        CreatedBy = UserId,
                        UpdatedBy = UserId
                    });
                }

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPost("full-legal-app-data-access")]
        public async Task<IActionResult> GrantFullDataAccess(
            [FromBody] LegalAppUpdateUserAccessForm form,
            [FromServices] AppDbContext context)
        {
            try
            {
                var requester = context.Users
                    .Include(x => x.AccessKey)
                    .FirstOrDefault(x => x.Id.Equals(UserId));

                var user = context.Users
                    .Include(x => x.AccessKey)
                    .FirstOrDefault(x => x.Id.Equals(form.UserId));

                if (requester is null ||
                    user is null ||
                    requester.AccessKey is null ||
                    user.AccessKey is null)
                {
                    return BadRequest();
                }

                if (!requester.AccessKey.Name.Equals(user.AccessKey.Name))
                {
                    return BadRequest();
                }

                context.DataAccesses.RemoveRange(
                    context.DataAccesses
                        .Where(x => x.UserId.Equals(user.Id)));

                var clients = context.LegalAppClients
                    .Include(x => x.LegalAppCases)
                    .Where(x =>
                        x.DataAccessKey.Equals(requester.AccessKey.Name))
                    .ToList();

                foreach (var client in clients)
                {
                    context.Add(new DataAccess
                    {
                        RestrictedType = RestrictedType.LegalAppClient,
                        ItemId = client.Id,
                        UserId = user.Id,
                        CreatedBy = UserId,
                        UpdatedBy = UserId
                    });

                    foreach (var cs in client.LegalAppCases)
                    {
                        context.Add(new DataAccess
                        {
                            RestrictedType = RestrictedType.LegalAppCase,
                            ItemId = cs.Id,
                            UserId = user.Id,
                            CreatedBy = UserId,
                            UpdatedBy = UserId
                        });
                    }
                }

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPost("revoke-legal-app-data-access")]
        public async Task<IActionResult> RevokeAllDataAccess(
            [FromBody] LegalAppUpdateUserAccessForm form,
            [FromServices] AppDbContext context)
        {
            try
            {
                var requester = context.Users
                    .Include(x => x.AccessKey)
                    .FirstOrDefault(x => x.Id.Equals(UserId));

                var user = context.Users
                    .Include(x => x.AccessKey)
                    .FirstOrDefault(x => x.Id.Equals(form.UserId));

                if (requester is null ||
                    user is null ||
                    requester.AccessKey is null ||
                    user.AccessKey is null)
                {
                    return BadRequest();
                }

                if (!requester.AccessKey.Name.Equals(user.AccessKey.Name))
                {
                    return BadRequest();
                }

                context.DataAccesses.RemoveRange(
                    context.DataAccesses
                        .Where(x => x.UserId.Equals(user.Id)));

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
        
        [HttpGet("legal-app-summary")]
        public async Task<ActionResult<AppSummaryViewModel>> GetLegalAppSummary(
            [FromServices] AppDbContext context,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            var result = new AppSummaryViewModel();

            var accessKey = await context.AccessKeys
                .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.Users.Any(y => y.Id.Equals(UserId)));

            if (accessKey is null)
            {
                return BadRequest();
            }
            
            var relatedUsers = context.Users
                .Include(x => x.AccessKey)
                .Where(x => x.AccessKey.Name.Equals(accessKey.Name))
                .ToList();
            
            var appData = context.LegalAppClients
                .Where(x => x.DataAccessKey.Equals(accessKey.Name))
                .Select(x => new
                {
                    x.Id,
                    x.LegalAppCases.Count
                })
                .ToList();

            result.ClientsCount = appData.Count;
            result.CasesCount = appData.Sum(x => x.Count);
            result.UsersCount = relatedUsers.Count;
            
            
            


            return Ok();
        }
    }
}