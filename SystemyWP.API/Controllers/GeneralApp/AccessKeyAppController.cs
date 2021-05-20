using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SystemyWP.API.Controllers.GeneralApp
{
    [Route("/api/portal/")]
    [Authorize(SystemyWpConstants.Policies.ClientAdmin)]
    public class AccessKeyAppController : ApiController
    {
        public AccessKeyAppController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }
    }
}