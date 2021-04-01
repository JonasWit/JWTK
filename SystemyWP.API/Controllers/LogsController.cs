using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Projections;
using SystemyWP.API.Services.PortalLoggerService;
using SystemyWP.Data;
using SystemyWP.Data.Enums;
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

        [HttpGet("logs/split/dates/")]
        public async Task<List<object>> ListLogRecords([FromServices] AppDbContext context, string from, string to,
            int cursor, int take, bool access, bool exception, bool admin, bool personalData, bool issue)
        {
            if (DateTime.TryParse(from, out DateTime fromDate) &&
                DateTime.TryParse(to, out DateTime toDate))
            {
                return await context.PortalLogs
                    .Where(x =>
                        (x.Created >= fromDate && x.Created <= toDate.AddDays(1)) &&
                        ((x.LogType == LogType.Access && access) ||
                         (x.LogType == LogType.Admin && admin) ||
                         (x.LogType == LogType.Exception && exception) ||
                         (x.LogType == LogType.Issue && issue) ||
                         (x.LogType == LogType.PersonalData && personalData)))
                    .OrderByDescending(x => x.Created)
                    .Skip(cursor)
                    .Take(take)
                    .Select(LogRecordProjection.Projection)
                    .ToListAsync();
            }
            else
            {
                return new List<object>();
            }
        }

        public LogsController(PortalLogger portalLogger) : base(portalLogger)
        {
        }
    }
}