using System;
using System.Security.Claims;

namespace SystemyWP.API
{
    public class SystemyWpConstants
    {
        public struct Policies
        {
            public const string Client = nameof(Client);
            public const string ClientAdmin = nameof(ClientAdmin);
            public const string PortalAdmin = nameof(PortalAdmin);
            public const string LegalAppAccess = nameof(LegalAppAccess);
        }
        
        public struct Claims
        {
            public const string Role = "Role";
            public const string AppAccess = "AppAccess";
            public static readonly Claim InvitedClaim = new Claim(Role, Roles.Invited);
            public static readonly Claim ClientClaim = new Claim(Role, Roles.Client);
            public static readonly Claim ClientAdminClaim = new Claim(Role, Roles.ClientAdmin);
            public static readonly Claim PortalAdminClaim = new Claim(Role, Roles.PortalAdmin);
            public static readonly Claim LegalAppAccessClaim = new Claim(AppAccess, Apps.LegalApp);
        }

        public struct Roles
        {
            public const string Invited = nameof(Invited);
            public const string Client = nameof(Client);
            public const string ClientAdmin = nameof(ClientAdmin);
            public const string PortalAdmin = nameof(PortalAdmin);
        }

        public struct Apps
        {
            public const string LegalApp = nameof(LegalApp);
        }

        public struct Files
        {
            public struct Providers
            {
                public const string Local = nameof(Local);
                public const string S3 = nameof(S3);
            }

            private const string ProfilePrefix = "profile_picture_";

            public static string GenerateProfileFileName()
            {
                return $"{ProfilePrefix}{DateTime.Now.Ticks}.jpg";
            }
        }
    }
}