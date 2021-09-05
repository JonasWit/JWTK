using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SystemyWP.API.CustomAttributes;
using SystemyWP.Data;
using SystemyWP.Data.Enums;
using SystemyWP.Data.Models.General;

namespace SystemyWP.API.Services.Logging
{
    [TransientService]
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
        
        public async Task Log(string endpoint, IdentityUser user, LogType logType, string description, string createdBy)
        {
            _context.PortalLogs.Add(new PortalLogRecord
            {
                Endpoint = endpoint,
                LogType = LogType.Access,
                Description = description,
                CreatedBy = createdBy,
                UserEmail = user.Email,
                UserName = user.UserName,
                UserId = user.Id,
            });
            await _context.SaveChangesAsync();
        }
    }
}