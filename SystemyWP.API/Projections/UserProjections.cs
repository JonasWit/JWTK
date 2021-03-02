using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SystemyWP.Data.Models;

namespace SystemyWP.API.Projections
{
    public static class UserProjections
    {
        public static Expression<Func<User, object>> UserProjection(string role, 
            string dataAccessKey, List<string> allowedApps) =>
            user => new
            {
                user.Id,
                user.Username,
                user.Image,
                Role = role,
                DataAccessKey = dataAccessKey,
                AllowedApps = allowedApps
            };
    }
}