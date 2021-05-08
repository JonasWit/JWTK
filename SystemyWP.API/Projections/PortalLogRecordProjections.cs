using System;
using System.Linq.Expressions;
using Systemywp.Data.Models.General;

namespace Systemywp.Api.Projections
{
    public static class PortalLogRecordProjections
    {
        public static Func<PortalLogRecord, object> Create => StandardProjection.Compile();
        public static Expression<Func<PortalLogRecord, object>> StandardProjection =>
            log => new
            {
                log.Description,
                ExceptionMessage  = string.IsNullOrEmpty(log.ExceptionMessage) ? null : log.ExceptionMessage,
                ExceptionStackTrace = string.IsNullOrEmpty(log.ExceptionStackTrace) ? null : log.ExceptionStackTrace,
                LogType = log.LogType.ToString(),
                UserId = log.CreatedBy,
                log.Endpoint,
                log.Created,
                log.Id,
                log.UserEmail
            };
    }
}