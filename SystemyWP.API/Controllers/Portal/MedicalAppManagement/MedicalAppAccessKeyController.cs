using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SystemyWP.API.Controllers.Portal.MedicalAppManagement
{
    [Route("/api/portal-admin/key-admin/medical-app")]
    [Authorize(SystemyWpConstants.Policies.PortalAdmin)]
    public class MedicalAppAccessKeyController : ApiController
    {
        public MedicalAppAccessKeyController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }
        
        
        
        
        
        
    }
}