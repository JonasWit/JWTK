namespace VappsMobile.AppConfig
{
    public static class ApiConfig
    {
        public struct ApiUrls
        {
            public const string MasterService = @"https://api.systemywp.pl";
        }

        public struct ApiControllers
        {
            public const string Auth = nameof(Auth);
            public const string Health = nameof(Health);
        }

        public struct HttpClientsNames
        {
            public const string AuthClient = nameof(AuthClient);
            public const string HealthClient = nameof(HealthClient);
        }
    }
}
