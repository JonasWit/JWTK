using System;
using System.Linq.Expressions;
using SystemyWP.Data.Models.General;

namespace SystemyWP.API.Projections
{
    public static class ApiLogRecordProjections
    {
        public static Func<ApiLogRecord, object> Create => StandardProjection.Compile();
        public static Expression<Func<ApiLogRecord, object>> StandardProjection =>
            log => new
            {
                log.Created,
                log.Id,
                log.Source,
                log.State,
                log.EventName,
                log.ExceptionMessage,
                LogLevel = log.LogLevel.ToString(),
                log.StackTrace
            };
    }
}