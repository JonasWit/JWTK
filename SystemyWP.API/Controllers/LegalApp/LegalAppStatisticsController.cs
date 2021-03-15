using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Services.PortalLoggerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace SystemyWP.API.Controllers.LegalApp
{
    [Route("/api/legal-app-statistics")]
    [Authorize(SystemyWPConstants.Policies.Client)]
    [Authorize(SystemyWPConstants.Policies.LegalAppAccess)]
    public class LegalAppStatistics : ApiController
    {
        public LegalAppStatistics(PortalLogger portalLogger) : base(portalLogger)
        {
        }
        
        
        
        
        
    }
}