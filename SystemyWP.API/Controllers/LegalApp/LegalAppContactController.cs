using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SystemyWP.API.Controllers.LegalApp
{
    [Route("/api/legal-app-contacts")]
    [Authorize(SystemyWpConstants.Policies.Client)]
    public class LegalAppContactController : LegalAppApiController
    {
        public LegalAppContactController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }
        
        
        
        
    }
}