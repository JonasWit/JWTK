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
