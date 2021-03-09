﻿using System;
using System.Linq;
using System.Linq.Expressions;
using SystemyWP.Data.Enums;
using SystemyWP.Data.Models;

namespace SystemyWP.API.Projections
{
    public static class LogRecordProjection
    {
        public static Func<PortalLog, object> Create => Projection.Compile();
        public static Expression<Func<PortalLog, object>> Projection =>
            log => new
            {
                log.Message,
                log.Username,
                log.ExceptionDetails,
                LogType = log.LogType.ToString(),
                log.SourceMethod,
                log.SourceType,
                log.UserId,
                log.Created,
                log.Id
            };
    }
}