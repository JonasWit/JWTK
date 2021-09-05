﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Projections;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data;
using SystemyWP.Data.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SystemyWP.API.Controllers.Portal
{
    [Route("/api/portal-admin/log-admin")]
    [Authorize(SystemyWpConstants.Policies.PortalAdmin)]
    public class LogsController : ApiController
    {
        public LogsController(PortalLogger portalLogger, AppDbContext context) : base(portalLogger, context)
        {
        }

        [HttpGet("logs")]
        public IEnumerable<object> ListPortalLogRecords()
        {
            return _context.PortalLogs
                .Select(PortalLogRecordProjections.StandardProjection)
                .ToList();
        }

        [HttpGet("logs/split")]
        public async Task<IActionResult> ListPortalLogRecords(int cursor, int take)
        {
            try
            {
                var result =  _context.PortalLogs
                    .OrderByDescending(x => x.Created)
                    .Skip(cursor)
                    .Take(take)
                    .Select(PortalLogRecordProjections.StandardProjection)
                    .ToList();

                return Ok(result);
            }
            catch (Exception e)
            {
                await HandleException(e);
                return ServerError;
            }
        }

        [HttpPost("logs/delete/{id}")]
        public async Task<IActionResult> DeletePortalLogRecord(long id)
        {
            var logRecord = _context.PortalLogs.FirstOrDefault(x => x.Id == id);

            if (logRecord is not null)
            {
                _context.Remove(logRecord);
                await _context.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("logs/split/dates")]
        public IEnumerable<object> ListPortalLogRecords(string from, string to,
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
                    .Select(PortalLogRecordProjections.StandardProjection)
                    .ToList();
            return new List<object>();
        }

        [HttpGet("logs/server")]
        public IEnumerable<object> ListApiLogRecords()
        {
            return _context.ApiLogs
                .Select(ApiLogRecordProjections.StandardProjection)
                .ToList();
        }

        [HttpGet("logs/server/split")]
        public IEnumerable<object> ListApiLogRecords(int cursor, int take)
        {
            return _context.ApiLogs
                .OrderByDescending(x => x.Created)
                .Skip(cursor)
                .Take(take)
                .Select(ApiLogRecordProjections.StandardProjection)
                .ToList();
        }

        [HttpGet("logs/server/split/dates/")]
        public IEnumerable<object> ListApiLogRecords(string from, string to,
            int cursor, int take, bool information, bool critical, bool debug, bool error, bool none, bool trace,
            bool warning)
        {
            if (DateTime.TryParse(from, out var fromDate) &&
                DateTime.TryParse(to, out var toDate))
                return _context.ApiLogs
                    .Where(x =>
                        x.Created >= fromDate && x.Created <= toDate.AddDays(1) &&
                        (x.LogLevel == LogLevel.Information && information ||
                         x.LogLevel == LogLevel.Critical && critical ||
                         x.LogLevel == LogLevel.Debug && debug ||
                         x.LogLevel == LogLevel.Error && error ||
                         x.LogLevel == LogLevel.None && none ||
                         x.LogLevel == LogLevel.Trace && trace ||
                         x.LogLevel == LogLevel.Warning && warning))
                    .OrderByDescending(x => x.Created)
                    .Skip(cursor)
                    .Take(take)
                    .Select(ApiLogRecordProjections.StandardProjection)
                    .ToList();
            return new List<object>();
        }
    }
}