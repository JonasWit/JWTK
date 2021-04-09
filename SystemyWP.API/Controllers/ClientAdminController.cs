﻿using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Services.PortalLoggerService;
using SystemyWP.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace SystemyWP.API.Controllers
{
    [Route("/api/portal-admin/user-admin")]
    [Authorize(SystemyWPConstants.Policies.PortalAdmin)]
    public class ClientAdminController : ApiController
    {
        public ClientAdminController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }
    }
}