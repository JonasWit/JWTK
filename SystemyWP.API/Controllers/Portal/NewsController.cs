using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SystemyWP.API.Controllers.Portal
{
    [Route("/api/portal-news")]
    [Authorize(SystemyWpConstants.Policies.PortalAdmin)]
    public class NewsController : ApiController
    {
        public NewsController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }
    }
}