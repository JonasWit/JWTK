using System;
using System.Threading.Tasks;
using SystemyWP.Data;
using SystemyWP.Data.Enums;
using SystemyWP.Data.Models;

namespace SystemyWP.API.Services.PortalLoggerService
{
    public class PortalLogger
    {
        private readonly AppDbContext _context;

        public PortalLogger(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task Log(LogType logType, string message, string sourceType, string sourceMethod, string userId, string userEmail, Exception ex)
        {
            _context.PortalLogs.Add(new PortalLog
            {
                LogType = logType,
                Message = message,
                SourceType = sourceType,
                SourceMethod = sourceMethod,
                ExceptionDetails = ex.Message,
                UserId = userId,
                Username = userEmail
            });
            await _context.SaveChangesAsync();
        }
        
        public async Task Log(LogType logType, string message, string sourceType, string sourceMethod, string userId, string userEmail)
        {
            _context.PortalLogs.Add(new PortalLog
            {
                LogType = logType,
                Message = message,
                SourceType = sourceType,
                SourceMethod = sourceMethod,
                UserId = userId,
                Username = userEmail
            });
            await _context.SaveChangesAsync();
        }
        
        public async Task Log(LogType logType, string message, string userId, string userEmail)
        {
            _context.PortalLogs.Add(new PortalLog
            {
                LogType = logType,
                Message = message,
                UserId = userId,
                Username = userEmail
            });
            await _context.SaveChangesAsync();
        }
        
        public async Task LogTest(LogType logType, string message, string userId, string userEmail, DateTime created)
        {
            _context.PortalLogs.Add(new PortalLog
            {
                LogType = logType,
                Message = message,
                UserId = userId,
                Username = userEmail,
                Created = created
            });
            await _context.SaveChangesAsync();
        }
    }
}