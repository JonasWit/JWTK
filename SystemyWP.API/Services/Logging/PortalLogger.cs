using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemyWP.API.Data;
using SystemyWP.API.Data.Enums;
using SystemyWP.API.Data.Models.General.Logging;
using SystemyWP.API.Data.Models.UsersManagement;

namespace SystemyWP.API.Services.Logging
{
    public class PortalLogger
    {
        private readonly AppDbContext _context;

        public PortalLogger(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        
        public async Task Log(PortalLogRecord logRecord)
        {
            _context.PortalLogs.Add(logRecord);
            await _context.SaveChangesAsync();
        }
        
        public async Task Log(string endpoint, string userId, LogType logType, string description)
        {
            _context.PortalLogs.Add(new PortalLogRecord
            {
                Endpoint = endpoint,
                LogType = LogType.Access,
                Description = description,
                UserEmail = userId,
            });
            await _context.SaveChangesAsync();
        }
        
        public async Task Log(string endpoint, string userId, LogType logType, Exception ex)
        {
            _context.PortalLogs.Add(new PortalLogRecord
            {
                ExceptionStackTrace = ex.StackTrace,
                Endpoint = endpoint,
                LogType = LogType.Access,
                Description = ex.Message,
                UserEmail = userId,
            });
            await _context.SaveChangesAsync();
        }
    }
}