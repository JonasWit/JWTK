using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Services.PortalLoggerService;
using Microsoft.AspNetCore.Authorization;

namespace SystemyWP.API.Controllers.LegalApp
{
    [Microsoft.AspNetCore.Components.Route("/api/legal-app-clients")]
    [Authorize]
    [Authorize(SystemyWPConstants.Policies.Client)]
    [Authorize(SystemyWPConstants.Policies.LegalAppAccess)]
    public class LegalAppClientController : ApiController
    {
        public LegalAppClientController(PortalLogger portalLogger) : base(portalLogger)
        {
        }

        // [HttpGet("clients")]
        // public IEnumerable<object> ListClient()
        // {
        //     
        //     
        //     
        //     
        //     
        //     
        //     
        //     
        //     
        //     
        //     
        // }
        
    }
}