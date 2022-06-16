using System;
using System.Security.Claims;

namespace SystemyWP.API.Constants;

public class AppConstants
{
    public static string ExceptionConsoleMessage(Exception e)
    {
        return $"--> Exception occured: {e.Message}";
    }

    public struct ResponseMessages
    {
        public const string DefaultExceptionMessage = "Error occured contact admin";
    }

    public struct CharacterSets
    {
        public const string StandardSet =
            @"ąęźżćłśĄĘĆŚŹŻŁabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";
    }

    public struct Paths
    {
        public const string LoginPath = @"/Account/Login";
        public const string LogoutPath = @"/api/auth/logout";
        public const string SecretSettings = @"secrets/appsettings.secrets.json";
    }

    public struct ServiceResponses
    {
        public const string AliveResponse = "Service is alive!";
        public const string DeadResponse = "Service is dead!";
        public const string ErrorResponse = "Service check gives error!";
    }

    public struct DataLimits
    {
        public const int DescriptionLimit = 1000;
        public const int NameLimit = 500;
        public const int ShortKeyLimit = 128;
        public const int KeyLimit = 256;
        public const int LongKeyLimit = 512;
    }

    public struct Services
    {
        public const string MasterService = nameof(MasterService);
        public const string GastronomyService = nameof(GastronomyService);
    }

    public struct AuthenticationType
    {
        public const string ServerAuth = nameof(ServerAuth);
    }

    public struct CorsName
    {
        public const string ClientApp = nameof(ClientApp);
    }

    public struct Patterns
    {
        public const string PasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{16,}$";
    }

    public struct Policies
    {
        public const string User = nameof(User);
        public const string Admin = nameof(Admin);
    }

    public struct Claims
    {
        public static readonly Claim UserClaim = new(ClaimTypes.Role, Roles.User);
        public static readonly Claim AdminClaim = new(ClaimTypes.Role, Roles.Admin);
    }

    public struct Roles
    {
        public const string User = nameof(User);
        public const string Admin = nameof(Admin);
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