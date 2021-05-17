using System;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Projections.LegalApp.Clients;
using SystemyWP.API.Projections.LegalApp.Contacts;
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
    [Route("/api/legal-app-client-contacts")]
    [Authorize(SystemyWpConstants.Policies.Client)]
    [Authorize(SystemyWpConstants.Policies.LegalAppAccess)]
    public class LegalAppClientContactController : LegalAppApiController
    {
        public LegalAppClientContactController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }
        
        [HttpGet("client/{clientId}/contacts")]
        public async Task<IActionResult> GetClient(int clientId)
        {
            try
            {
                var check = await CheckAccess(RestrictedType.LegalAppClient, clientId);
                if (check.AccessKey is null) return StatusCode(StatusCodes.Status403Forbidden);

                if (check.DataAccessAllowed)
                {
                    var result = _context.LegalAppClients
                        .Include(x => x.AccessKey)
                        .Where(x => x.AccessKey.Id == check.AccessKey.Id && x.Id == clientId)
                        .Include(x => x.Contacts)
                        .ThenInclude(x => x.Emails)
                        .Include(x => x.Contacts)
                        .ThenInclude(x => x.PhoneNumbers)
                        .Include(x => x.Contacts)
                        .ThenInclude(x => x.PhysicalAddresses)
                        .Select(x => LegalAppContactProjections.BasicProjection)
                        .ToList();
                    
                    return Ok(result);
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