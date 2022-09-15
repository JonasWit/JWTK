namespace VappsMobile.AppConfig
{
    public static class ApiConfig
    {
        public struct ApiUrls
        {
            public const string MasterService = @"https://api.systemywp.pl";
        }

        public struct ApiAuthController
        {
            public const string BasePath = "auth";
            public const string Authenticate = "authenticate";
            public const string Register = "register";
            public const string DeleteAccount = "delete-account";
            public const string ForgotPassword = "reset-password-request";
        }

        public struct ApiHealthController
        {
            public const string BasePath = "health";
        }

        public struct HttpClientsNames
        {
            public const string AuthClient = nameof(AuthClient);
            public const string HealthClient = nameof(HealthClient);
        }
    }
}
