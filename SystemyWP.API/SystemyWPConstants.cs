using System;
using System.Security.Claims;

namespace SystemyWP.API
{
    public class SystemyWPConstants
    {
        public struct Policies
        {
            public const string Client = nameof(Client);
            public const string ClientAdmin = nameof(ClientAdmin);
            public const string PortalAdmin = nameof(PortalAdmin);
            public const string AppAccess = nameof(AppAccess);
            public const string DataAccess = nameof(DataAccess);
        }

        public struct IdentityResources
        {
            public const string RoleScope = "role";
        }

        public struct Claims
        {
            public const string Role = "role";
            public static readonly Claim ClientClaim = new Claim(Role, Roles.Client);
            public const string AppAccess = "appAccess";
            public const string DataAccessKey = "dataAccess";
        }

        public struct Roles
        {
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

            private const string ProfilePrefix = "p_";

            public static string GenerateProfileFileName()
            {
                return $"{ProfilePrefix}{DateTime.Now.Ticks}.jpg";
            }
        }
    }
}