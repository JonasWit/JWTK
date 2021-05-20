using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Projections.General;
using SystemyWP.API.Projections.LegalApp.Clients;
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
    [Route("/api/legal-app-clients-finance")]
    [Authorize(SystemyWpConstants.Policies.Client)]
    public class LegalAppClientFinanceController : LegalAppApiController
    {
        public LegalAppClientFinanceController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger,
            context)
        {
        }

        [HttpGet("client/{clientId}/finance-records")]
        public async Task<IActionResult> GetFinanceRecords(long clientId, string from, string to)
        {
            try
            {
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (check.DataAccessAllowed)
                {
                    var client = _context.LegalAppClients
                        .Include(x => x.LegalAppClientWorkRecord)
                        .Include(x => x.AccessKey)
                        .FirstOrDefault(x => x.Id == clientId && x.AccessKey.Id == check.AccessKey.Id);

                    if (client is null) return BadRequest();

                    if (DateTime.TryParse(from, out var fromDate) &&
                        DateTime.TryParse(to, out var toDate))
                    {
                        var result = client.LegalAppClientWorkRecord
                            .Select(LegalAppClientFinanceProjections.CreateBasic)
                            .ToList();

                        return Ok(result);
                        
                    }

                    return BadRequest();
                }

                return StatusCode(StatusCodes.Status403Forbidden);
            }
            catch (Exception e)
            {
                await LogException(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}