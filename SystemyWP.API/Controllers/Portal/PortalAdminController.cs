using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;

namespace SystemyWP.API.Controllers.Portal.Admin
{
    [Route("/api/[controller]")]
    [Authorize(SystemyWpConstants.Policies.PortalAdmin)]
    public class PortalAdminController : ApiControllerBase
    {
        public PortalAdminController(PortalLogger portalLogger, AppDbContext context, IMapper mapper) : base(portalLogger, context, mapper)
        {
        }
    }
}