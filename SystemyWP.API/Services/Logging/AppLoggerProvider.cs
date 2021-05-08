﻿using SystemyWP.Data;
using Microsoft.Extensions.Logging;

namespace SystemyWP.API.Services.Logging
{
    public class AppLoggerProvider : ILoggerProvider
    {
        private readonly AppDbContext _context;

        public AppLoggerProvider(AppDbContext context)
        {
            _context = context;
        }
        
        public void Dispose()
        {

        }

        public ILogger CreateLogger(string categoryName) => new DbLogger(_context);
    }
}