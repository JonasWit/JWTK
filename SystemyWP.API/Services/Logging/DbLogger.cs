using System;
using SystemyWP.Data;
using SystemyWP.Data.Models.General;
using Microsoft.Extensions.Logging;

namespace SystemyWP.API.Services.Logging
{
    public class DbLogger : ILogger
    {
        private readonly AppDbContext _context;

        public DbLogger(AppDbContext context)
        {
            _context = context;
        }
        
        public IDisposable BeginScope<TState>(TState state) => null;
 
        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(
            LogLevel logLevel, 
            EventId eventId, 
            TState state, 
            Exception exception, 
            Func<TState, Exception, string> formatter)
        {
            var log = new ApiLogRecord();
            log.LogLevel = logLevel;
            log.EventName = eventId.Name;
            log.State = state?.ToString();
            log.ExceptionMessage = exception?.Message;
            log.StackTrace = exception?.StackTrace;
            log.Source = "Server App";
            log.Created = DateTime.UtcNow;

            _context.Add(log);
            _context.SaveChanges();
        }
    }
}