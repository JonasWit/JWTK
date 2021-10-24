using Microsoft.AspNetCore.Authorization;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;

namespace SystemyWP.API.Controllers.BaseClases
{
    [Authorize]
    [Authorize(SystemyWpConstants.Policies.MedicalAppAccess)]
    public abstract class MedicalAppApiController : ApiController
    {
        protected MedicalAppApiController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }
    }
}