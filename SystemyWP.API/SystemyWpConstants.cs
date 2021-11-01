using System;
using System.Security.Claims;

namespace SystemyWP.API
{
    public class SystemyWpConstants
    {
        public struct CharacterSets
        {
            public const string StandardSet = @"ąęźżćłśĄĘĆŚŹŻŁabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";
        }
        
        public struct Paths
        {
            public const string LoginPath = @"/Account/Login";
            public const string LogoutPath = @"/api/auth/logout";
        }
        
        public struct CookiesNames
        {
            public const string IdCookie = @"systemywp_id";
        }
        
        public struct Patterns
        {
            public const string PasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{16,}$";
        }
        public struct ResponseMessages
        {
            public const string IncorrectParameters = "Niepoprawne zapytanie!";
            public const string NoAccess = "Brak dostępu!";
            public const string IncorrectBehaviour = "Zapytanie nie zostało obsłużone poprawnie!";
            public const string DataAlreadyExists = "Te dane już istnieją!";
            public const string DataNotFound = "Zapytanie nie zostało obsłużone poprawnie - nie znaleziono potrzebnych danych!";
            public const string AlreadyGranted = "Dostęp został już udzielony!";
            public const string AlreadyRevoked = "Dostęp został już odebrany!";
        }
        
        public struct Policies
        {
            public const string User = nameof(User);
            public const string UserAdmin = nameof(UserAdmin);
            public const string PortalAdmin = nameof(PortalAdmin);
            public const string LegalAppAccess = nameof(LegalAppAccess);
            public const string MedicalAppAccess = nameof(MedicalAppAccess);
            public const string RestaurantAppAccess = nameof(RestaurantAppAccess);
        }
        
        public struct Claims
        {
            public const string Role = "Role";
            public const string AppAccess = "AppAccess";
            public static readonly Claim InvitedClaim = new(Role, Roles.Invited);
            public static readonly Claim UserClaim = new (Role, Roles.User);
            public static readonly Claim UserAdminClaim = new (Role, Roles.UserAdmin);
            public static readonly Claim PortalAdminClaim = new (Role, Roles.PortalAdmin);
            public static readonly Claim LegalAppAccessClaim = new (AppAccess, Apps.LegalApp);
            public static readonly Claim MedicalAppAccessClaim = new (AppAccess, Apps.MedicalApp);
            public static readonly Claim RestaurantAppAccessClaim = new (AppAccess, Apps.RestaurantApp);
        }

        public struct Roles
        {
            public const string Invited = nameof(Invited);
            public const string User = nameof(User);
            public const string UserAdmin = nameof(UserAdmin);
            public const string PortalAdmin = nameof(PortalAdmin);
        }

        public struct Apps
        {
            public const string LegalApp = nameof(LegalApp);
            public const string MedicalApp = nameof(MedicalApp);
            public const string RestaurantApp = nameof(RestaurantApp);
        }
        
        public struct Emails
        {
            public const string SupportAddress = "wsparcie@systemywp.pl";
            public const string ContactAddress = "kontakt@systemywp.pl";
            public const string OfficeAddress = "biuro@systemywp.pl";
        }

        public struct Files
        {
            public struct Providers
            {
                public const string Local = nameof(Local);
                public const string S3 = nameof(S3);
            }

            private const string ProfilePrefix = "prof_pic_";
            private const string PortalPublicationPrefix = "portal_pub_";

            public static string GenerateProfileFileName()
            {
                return $"{ProfilePrefix}{DateTime.Now.Ticks}.jpg";
            }
            
            public static string GeneratePortalPublicationFileName()
            {
                return $"{PortalPublicationPrefix}{DateTime.Now.Ticks}.jpg";
            }
        }
    }
}