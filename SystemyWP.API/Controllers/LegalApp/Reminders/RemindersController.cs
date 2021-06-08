using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SystemyWP.API.Controllers.LegalApp.Reminders
{
    [Route("/api/legal-app-reminders")]
    [Authorize(SystemyWpConstants.Policies.Client)]
    public class RemindersController : LegalAppApiController
    {
        public RemindersController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }
        
        
        
        
        
        
        
        
        
        
    }
}