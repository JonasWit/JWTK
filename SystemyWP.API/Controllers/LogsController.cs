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
        public Task<List<object>> ListLogRecords([FromServices] AppDbContext context, string from, string to,
            int cursor, int take, bool access, bool exception, bool admin, bool personalData, bool issue)
        {
            if (DateTime.TryParse(from, out DateTime fromDate) &&
                DateTime.TryParse(to, out DateTime toDate))
            {
                var types = new Dictionary<LogType, bool>
                {
                    {LogType.Access, access},
                    {LogType.Admin, admin},
                    {LogType.Exception, exception},
                    {LogType.Issue, issue},
                    {LogType.PersonalData, personalData}
                };

                return context.PortalLogs
                    .Where(x =>
                        (x.Created >= fromDate.AddDays(1) && x.Created <= toDate.AddDays(1)) &&
                        (types[x.LogType] == true))
                    .OrderByDescending(x => x.Created)
                    .Skip(cursor)
                    .Take(take)
                    .Select(LogRecordProjection.Projection)
                    .ToListAsync();
            }
            else
            {
                return Task.FromResult(new List<object>());
            }
        }

        public LogsController(PortalLogger portalLogger) : base(portalLogger)
        {
        }
    }
}