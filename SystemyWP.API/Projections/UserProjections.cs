using System;
using System.Linq.Expressions;
using SystemyWP.Data.Models.General;

namespace SystemyWP.API.Projections
{
    public static class UserProjections
    {
        public static Expression<Func<User, object>> UserProjection(string userName, string role, bool legalAppAllowed) =>
            user => new
            {
                user.Id,
                Username = userName,
                user.Image,
                Role = role,
                DataAccessKey = AccessKeyProjection.CreateFlat(user.AccessKey),
                LegalAppAllowed = legalAppAllowed,
                user.PhoneNumber,
                user.Address,
                user.City,
                user.Country,
                user.Name,
                user.Surname,
                user.Vivodership,
                user.AddressCorrespondence,
                user.PostCode,
                user.CompanyFullName,
                user.KRS,
                user.NIP,
                user.REGON
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