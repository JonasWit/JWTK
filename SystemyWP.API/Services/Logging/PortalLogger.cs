using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SystemyWP.API.CustomAttributes;
using SystemyWP.API.Data;
using SystemyWP.API.Data.Enums;
using SystemyWP.API.Data.Models.General.Logging;

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
                UserEmail = user.Email,
                UserId = user.Id,
            });
            await _context.SaveChangesAsync();
        }
    }
}