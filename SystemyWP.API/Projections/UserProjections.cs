using System;
using System.Linq.Expressions;
using SystemyWP.Data.Models;

namespace SystemyWP.API.Projections
{
    public static class UserProjections
    {
        public static Expression<Func<User, object>> UserProjection(string userName, string role, bool legalAppAllowed) =>
            user => new
            {
                Id = user.Id,
                Username = userName,
                Image = user.Image,
                Role = role,
                DataAccessKey = user.AccessKey,
                LegalAppAllowed = legalAppAllowed
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
            public object DataAccessKey { get; set; }
            public bool  LegalAppAllowed { get; set; }
            public bool Locked { get; set; }    
        }
    }
}