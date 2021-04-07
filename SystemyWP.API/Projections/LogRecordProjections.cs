using System;
using System.Linq.Expressions;
using SystemyWP.Data.Models.General;

namespace SystemyWP.API.Projections
{
    public static class LogRecordProjection
    {
        public static Func<PortalLog, object> Create => Projection.Compile();
        public static Expression<Func<PortalLog, object>> Projection =>
            log => new
            {
                log.Description,
                ExceptionMessage  = string.IsNullOrEmpty(log.ExceptionMessage) ? null : log.ExceptionMessage,
                ExceptionStackTrace = string.IsNullOrEmpty(log.ExceptionStackTrace) ? null : log.ExceptionStackTrace,
                LogType = log.LogType.ToString(),
                UserId = log.CreatedBy,
                log.Created,
                log.Id,
                log.UserEmail
            };
    }
}