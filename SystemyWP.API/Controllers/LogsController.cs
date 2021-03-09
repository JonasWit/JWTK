using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Projections;
using SystemyWP.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Controllers
{
    [Route("/api/portal-admin/log-admin")]
    [Authorize(SystemyWPConstants.Policies.PortalAdmin)]
    public class LogsController : ApiController
    {
        [HttpGet("logs")]
        public Task<List<object>> ListLogRecords([FromServices] AppDbContext context) =>
            context.PortalLogs
                .Select(LogRecordProjection.Projection)
                .ToListAsync();
        
        [HttpGet("logs/split")]
        public Task<List<object>> ListLogRecords(
            [FromServices] AppDbContext context,
            int cursor,
            int take) =>
            context.PortalLogs
                .OrderByDescending(x => x.Created)
                .Skip(cursor)
                .Take(take)
                .Select(LogRecordProjection.Projection)
                .ToListAsync();    
        
    }
}