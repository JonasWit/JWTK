using Systemywp.Api.Controllers.BaseClases;
using Systemywp.Api.Services.Logging;
using Systemywp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Systemywp.Api.Controllers.LegalApp
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