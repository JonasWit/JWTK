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

        public async Task Log(LogType logType, string endpoint, string userId, string userEmail, string description = "", string exceptionMessage = "", string exceptionStackTrace = "")
        {
            _context.PortalLogs.Add(new PortalLog
            {
                LogType = logType,
                Description = description,
                UserId = userId,
                UserEmail = userEmail,
                Endpoint = endpoint,
                ExceptionMessage = exceptionMessage,
                ExceptionStackTrace = exceptionStackTrace,
            });
            await _context.SaveChangesAsync();
        }
    }
}