using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Services.PortalLoggerService;
using SystemyWP.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SystemyWP.API.Controllers
{
    [Route("/api/news")]
    [Authorize(SystemyWPConstants.Policies.PortalAdmin)]
    public class NewsController : ApiController
    {
        public NewsController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }
    }
}