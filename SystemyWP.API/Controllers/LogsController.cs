using System;
using System.Collections.Generic;
using System.Linq;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Projections;
using SystemyWP.API.Services.PortalLoggerService;
using SystemyWP.Data;
using SystemyWP.Data.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SystemyWP.API.Controllers
{
    [Route("/api/portal-admin/log-admin")]
    [Authorize(SystemyWPConstants.Policies.PortalAdmin)]
    public class LogsController : ApiController
    {
        public LogsController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }

        [HttpGet("logs")]
        public IEnumerable<object> ListLogRecords()
        {
            return _context.PortalLogs
                .Select(LogRecordProjection.Projection)
                .ToList();
        }

        [HttpGet("logs/split")]
        public IEnumerable<object> ListLogRecords(int cursor, int take)
        {
            return _context.PortalLogs
                .OrderByDescending(x => x.Created)
                .Skip(cursor)
                .Take(take)
                .Select(LogRecordProjection.Projection)
                .ToList();
        }

        [HttpPost("logs/delete/{id}")]
        public IActionResult DeleteLogRecord(long id)
        {
            var logRecord = _context.PortalLogs.FirstOrDefault(x => x.Id == id);

            if (logRecord is not null)
            {
                //todo: do this
            }

            return Ok();
        }

        [HttpGet("logs/split/dates/")]
        public IEnumerable<object> ListLogRecords(string from, string to,
            int cursor, int take, bool access, bool exception, bool admin, bool personalData, bool issue)
        {
            if (DateTime.TryParse(from, out var fromDate) &&
                DateTime.TryParse(to, out var toDate))
                return _context.PortalLogs
                    .Where(x =>
                        x.Created >= fromDate && x.Created <= toDate.AddDays(1) &&
                        (x.LogType == LogType.Access && access ||
                         x.LogType == LogType.Admin && admin ||
                         x.LogType == LogType.Exception && exception ||
                         x.LogType == LogType.Issue && issue ||
                         x.LogType == LogType.PersonalData && personalData))
                    .OrderByDescending(x => x.Created)
                    .Skip(cursor)
                    .Take(take)
                    .Select(LogRecordProjection.Projection)
                    .ToList();
            return new List<object>();
        }
    }
}