using System;
using System.Linq;
using System.Linq.Expressions;
using SystemyWP.Data.DataAccessModifiers;
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
                user.LightMode,
                Role = role,
                DataAccessKey = user.AccessKey == null ? null : AccessKeyProjection.CreateFlat(user.AccessKey),
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
        
        public static Expression<Func<User, object>> RelatedUserProjection(string userName, string email, string role) =>
            user => new
            {
                user.Id,
                Email = email,
                Username = userName,
                user.Image,
                user.LightMode,
                Role = role,
                DataAccessKey = user.AccessKey == null ? null : AccessKeyProjection.CreateFlat(user.AccessKey),
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
                user.REGON,
                DataAccess = user.DataAccess
                    .AsQueryable()
                    .Where(x => 
                        x.RestrictedType.Equals(RestrictedType.LegalAppCase) ||
                        x.RestrictedType.Equals(RestrictedType.LegalAppClient))
                    .Select(DataAccessProjections.Projection)
                    .ToList()
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