using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Projections.LegalApp;
using SystemyWP.API.Services.PortalLoggerService;
using SystemyWP.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers.LegalApp
{
    [Microsoft.AspNetCore.Components.Route("/api/legal-app-clients")]
    [Authorize(SystemyWPConstants.Policies.Client)]
    [Authorize(SystemyWPConstants.Policies.LegalAppAccess)]
    public class LegalAppClientController : ApiController
    {
        public LegalAppClientController(PortalLogger portalLogger) : base(portalLogger)
        {
        }

        [HttpGet("clients")]
        public ActionResult<IEnumerable<object>> ListClient([FromServices] AppDbContext context)
        {
            var user = context.Users
                .Include(x => x.AccessKey)
                .FirstOrDefault(x => x.Id.Equals(UserId));

            if (user.AccessKey is null)
            {
                return BadRequest("Brak klucza!");
            }

            var result = new List<object>();

            if (Role.Equals(SystemyWPConstants.Roles.ClientAdmin) ||
                Role.Equals(SystemyWPConstants.Roles.PortalAdmin))
            {
                result.AddRange(context.LegalAppClients
                    .Where(x => x.DataAccessKey.Equals(user.AccessKey))
                    .Select(LegalAppClientProjection.FlatProjection)
                    .ToList());
                
                return result;
            }
            else if(Role.Equals(SystemyWPConstants.Roles.Client))
            {
                //todo: add validation with DataAccessTable
                return Ok(result);
            }
            else
            {
                return BadRequest("Brak dostępu!");     
            }
        }
    }
}