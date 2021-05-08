using Systemywp.Api.Controllers.BaseClases;
using Systemywp.Api.Services.Logging;
using Systemywp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Systemywp.Api.Controllers.Portal
{
    [Route("/api/news")]
    [Authorize(SystemyWpConstants.Policies.PortalAdmin)]
    public class NewsController : ApiController
    {
        public NewsController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }
    }
}