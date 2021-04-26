using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Projections.LegalApp;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.LegalApp
{
    [Route("/api/legal-app-cases")]
    [Authorize(SystemyWpConstants.Policies.Client)]
    [Authorize(SystemyWpConstants.Policies.LegalAppAccess)]
    public class LegalAppCaseController : LegalAppApiController
    {
        public LegalAppCaseController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }

        [HttpGet("cases/{clientId}")]
        public async Task<IActionResult> GetCasesForClient(long clientId, int cursor, int take)
        {
            try
            {
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (Role.Equals(SystemyWpConstants.Roles.ClientAdmin) ||
                    Role.Equals(SystemyWpConstants.Roles.PortalAdmin))
                {
                    var cases = _context.LegalAppCases
                        .Include(x => x.LegalAppClient)
                        .ThenInclude(x => x.AccessKey)
                        .Where(x => x.LegalAppClient.Id == clientId && 
                                    x.LegalAppClient.AccessKey.Id == check.AccessKey.Id)
                        .OrderByDescending(x => x.Name)
                        .Skip(cursor)
                        .Take(take)
                        .Select(LegalAppCaseProjections.MinimalProjection)
                        .ToList();

                    return Ok(cases);
                }

                //Get data as User
                if (Role.Equals(SystemyWpConstants.Roles.Client))
                {
                    if (check.DataAccessAllowed)
                    {
                        var cases = _context.LegalAppCases
                            .Include(x => x.LegalAppClient)
                            .ThenInclude(x => x.AccessKey)
                            .Where(x => 
                                        x.LegalAppClient.Id == clientId &&
                                        x.LegalAppClient.AccessKey.Id == check.AccessKey.Id &&
                                        _context.DataAccesses
                                            .Where(y => y.UserId.Equals(UserId) &&
                                                        y.RestrictedType == RestrictedType.LegalAppCase)
                                                .Any(y => y.ItemId == x.Id))
                            .OrderByDescending(x => x.Name)
                            .Skip(cursor)
                            .Take(take)
                            .Select(LegalAppCaseProjections.MinimalProjection)
                            .ToList();

                        return Ok(cases);
                    }
                }
                
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            catch (Exception e)
            {
                await _portalLogger
                    .Log(LogType.Exception, HttpContext.Request.Path.Value, UserId, UserEmail, e.Message, e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}