using System;
using System.Linq.Expressions;
using SystemyWP.API.Projections.General;
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
        
        public static Expression<Func<User, object>> UserProfileProjection(
            string role, 
            bool legalAppAllowed,
            bool medicalAppAllowed,
            bool restaurantAppAllowed) =>
            user => new
            {
                user.Id,
                user.Username,
                user.Image,
                Role = role,
                LegalAppDataAccessKey = user.LegalAccessKey == null ? null : AccessKeyProjection.CreateFlat(user.LegalAccessKey),
                RestaurantAppDataAccessKey = user.RestaurantAccessKey == null ? null : AccessKeyProjection.CreateFlat(user.RestaurantAccessKey),
                MedicalAppDataAccessKey = user.MedicalAccessKey == null ? null : AccessKeyProjection.CreateFlat(user.MedicalAccessKey),
                MedicalAppAllowed = medicalAppAllowed,
                LegalAppAllowed = legalAppAllowed,
                RestaurantAppAllowed = restaurantAppAllowed,
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
        
        public static Expression<Func<User, object>> RelatedUserProjection(string role) =>
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
            public object RestaurantAppDataAccessKey { get; set; }
            public bool  LegalAppAllowed { get; set; }
            public bool  MedicalAppAllowed { get; set; }
            public bool  RestaurantAppAllowed { get; set; }
            public bool Locked { get; set; }
        }
    }
}