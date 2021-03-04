using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SystemyWP.Data.Models;

namespace SystemyWP.API.Projections
{
    public static class UserProjections
    {
        public static Expression<Func<User, object>> UserProjection(string userName, string role, 
            string dataAccessKey, List<string> allowedApps) =>
            user => new
            {
                user.Id,
                Username = userName,
                user.Image,
                Role = role,
                DataAccessKey = dataAccessKey,
                AllowedApps = allowedApps
            };

        public class UserViewModel
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }
            public string Image { get; set; }           
            public string Role { get; set; }  
            public bool EmailConfirmed { get; set; }  
            public bool PolicyAccepted { get; set; }  
            public bool RulesAccepted { get; set; }  
            public string DataAccessKey { get; set; }
            public List<string> AllowedApps { get; set; } = new List<string>();   
            public bool Locked { get; set; }    
        }
    }
}