using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace SystemyWP.API.Data.Models.General.Logging
{
    public class ApiLogRecord
    {
        public long Id { get; set; }
        public LogLevel LogLevel { get; set; }
        public string EventName { get; set; }
        public string State { get; set; }
        public string Source { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        [Required]
        public DateTime Created { get; set; }
    }
}