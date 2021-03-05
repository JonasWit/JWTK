using SystemyWP.API.Controllers.BaseClases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace SystemyWP.API.Controllers
{
    [Route("/api/legal-app-clients")]
    [Authorize(SystemyWPConstants.Policies.LegalAppAccess)]
    public class LegalAppClientController : ApiController
    {
        
    }
}