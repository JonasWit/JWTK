using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Forms.LegalApp;
using SystemyWP.API.Projections.LegalApp;
using SystemyWP.API.Services.PortalLoggerService;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.LegalAppModels.Clients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.LegalApp
{
    [Route("/api/legal-app-clients")]
    [Authorize(SystemyWPConstants.Policies.Client)]
    [Authorize(SystemyWPConstants.Policies.LegalAppAccess)]
    public class LegalAppClientController : ApiController
    {
        public LegalAppClientController(PortalLogger portalLogger) : base(portalLogger)
        {
        }

        [HttpGet("client/{clientId}")]
        public ActionResult<object> GetClient(
            [FromServices] AppDbContext context,
            int clientId)
        {
            var user = context.Users
                .Include(x => x.AccessKey)
                .FirstOrDefault(x => x.Id.Equals(UserId));

            if (user?.AccessKey is null) return BadRequest("Brak klucza!");

            //Get data as Admin
            if (Role.Equals(SystemyWPConstants.Roles.ClientAdmin) ||
                Role.Equals(SystemyWPConstants.Roles.PortalAdmin))
            {
                var result = context.LegalAppClients
                    .Where(x =>
                        x.AccessKey.Name.Equals(user.AccessKey.Name))
                    .Select(LegalAppClientProjections.FlatProjection)
                    .FirstOrDefault();

                return Ok(result);
            }

            //Get data as User
            if (Role.Equals(SystemyWPConstants.Roles.Client))
            {
                var result = context.LegalAppClients
                    .Include(x => x.AccessKey)
                    .Where(x =>
                        x.AccessKey.Name.Equals(user.AccessKey.Name) &&
                        context.DataAccesses.Where(x => x.UserId.Equals(UserId))
                            .Any(y => y.RestrictedType == RestrictedType.LegalAppClient &&
                                      y.ItemId == x.Id))
                    .Select(LegalAppClientProjections.FlatProjection)
                    .FirstOrDefault();

                if (result is null) return StatusCode(403);

                return Ok(result);
            }

            return BadRequest("Wystąpił błąd!");
        }

        [HttpGet("clients")]
        public ActionResult<IEnumerable<object>> GetClientsData([FromServices] AppDbContext context)
        {
            var user = context.Users
                .Include(x => x.AccessKey)
                .FirstOrDefault(x => x.Id.Equals(UserId));

            if (user?.AccessKey is null) return BadRequest("Brak klucza!");

            var result = new List<object>();

            //Get data as Admin
            if (Role.Equals(SystemyWPConstants.Roles.ClientAdmin) ||
                Role.Equals(SystemyWPConstants.Roles.PortalAdmin))
            {
                result.AddRange(context.LegalAppClients
                    .Include(x => x.AccessKey)
                    .Where(x =>
                        x.AccessKey.Name.Equals(user.AccessKey.Name))
                    .Select(LegalAppClientProjections.FlatProjection)
                    .ToList());

                return Ok(result);
            }

            //Get data as User
            if (Role.Equals(SystemyWPConstants.Roles.Client))
            {
                //var allowedData = context.DataAccesses.Where(x => x.UserId.Equals(UserId));

                result.AddRange(context.LegalAppClients
                    .Include(x => x.AccessKey)
                    .Where(x =>
                        x.AccessKey.Name.Equals(user.AccessKey.Name) &&
                        context.DataAccesses.Where(x => x.UserId.Equals(UserId))
                            .Any(y => y.RestrictedType == RestrictedType.LegalAppClient &&
                                      y.ItemId == x.Id))
                    .Select(LegalAppClientProjections.FlatProjection)
                    .ToList());

                return Ok(result);
            }

            return BadRequest("Brak dostępu!");
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateClient(
            [FromBody] CreateClientForm form,
            [FromServices] AppDbContext context)
        {
            var user = await context.Users
                .Include(x => x.AccessKey)
                .FirstOrDefaultAsync(x => x.Id.ToLower().Equals(UserId.ToLower()));
            
            if (user?.AccessKey.Id != form.AccessKeyId)
            {
                return BadRequest("Klucz dostępu nie pasuje!");
            }

            context.Add(new LegalAppClient
            {
                AccessKey = user.AccessKey,
                Name = form.Name,
                CreatedBy = UserId,
                UpdatedBy = UserId
            });

            return Ok();
        }

        [HttpGet("admin/flat")]
        [Authorize(SystemyWPConstants.Policies.ClientAdmin)]
        public async Task<ActionResult<IEnumerable<object>>> GetClientsAndCasesForAccess(
            [FromServices] AppDbContext context)
        {
            try
            {
                var user = await context.Users
                    .Where(x => x.Id.Equals(UserId))
                    .Include(x => x.AccessKey)
                    .FirstOrDefaultAsync();

                var result = await context.LegalAppClients
                    .Include(x => x.AccessKey)
                    .Where(x =>
                        x.AccessKey.Name.Equals(user.AccessKey.Name))
                    .Include(x =>
                        x.LegalAppCases)
                    .Select(LegalAppClientProjections.MinimalProjection)
                    .ToListAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}