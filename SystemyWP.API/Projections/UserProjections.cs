using System;
using System.Linq;
using System.Linq.Expressions;
using SystemyWP.API.Projections.LegalApp.LegalAppAdmin;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.General;

namespace SystemyWP.API.Projections
{
    public static class UserProjections
    {
        public static Expression<Func<User, object>> BasicUserProjection =>
            user => new
            {
                user.Username,
                user.Id,
                user.Email,
            };
        
        public static Expression<Func<User, object>> BasicUserProjectionWithRole(string role) =>
            user => new
            {
                user.Username,
                user.Id,
                user.Email,
                Role = role
            };
        
        public static Expression<Func<User, object>> LegalAppUserProjection(string role, bool legalAppAllowed) =>
            user => new
            {
                user.Id,
                user.Username,
                user.Image,
                Role = role,
                LegalAppDataAccessKey = user.LegalAppAccessKey == null ? null : AccessKeyProjection.CreateFlat(user.LegalAppAccessKey),
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
                KRS = user.Krs,
                NIP = user.Nip,
                REGON = user.Regon,
                user.LastLogin
            };
        
        public static Expression<Func<User, object>> RelatedLegalAppUserProjection(string role) =>
            user => new
            {
                user.Id,
                user.Email,
                user.Username,
                user.Image,
                Role = role,
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
                KRS = user.Krs,
                NIP = user.Nip,
                REGON = user.Regon,
                user.LastLogin,
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
            public object LegalAppDataAccessKey { get; set; }
            public object MedicalAppDataAccessKey { get; set; }
            public bool  LegalAppAllowed { get; set; }
            public bool  MedicalAppAllowed { get; set; }
            public bool Locked { get; set; }
        }
    }
}