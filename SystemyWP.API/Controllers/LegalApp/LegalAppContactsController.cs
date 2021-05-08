using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SystemyWP.API.Controllers.LegalApp
{
    [Route("/api/legal-app-clients")]
    [Authorize(SystemyWpConstants.Policies.Client)]
    [Authorize(SystemyWpConstants.Policies.LegalAppAccess)]
    public class LegalAppcontactsController : LegalAppApiController
    {
        public LegalAppcontactsController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }
        
        
        
        
    }
}