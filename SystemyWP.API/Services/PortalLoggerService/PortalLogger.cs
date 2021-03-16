using System.Threading.Tasks;
using SystemyWP.Data;
using SystemyWP.Data.Enums;
using SystemyWP.Data.Models.General;

namespace SystemyWP.API.Services.PortalLoggerService
{
    public class PortalLogger
    {
        private readonly AppDbContext _context;

        public PortalLogger(AppDbContext appDbContext)
        {
            _context = appDbContext;
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

        public async Task Log(LogType logType, string message, string exceptionDetails, string userId, string userName)
        {
            _context.PortalLogs.Add(new PortalLog
            {
                LogType = logType,
                Message = message,
                StackTrace = exceptionDetails,
                UserId = userId,
                Username = userName
            });
            await _context.SaveChangesAsync();
        }
    }
}