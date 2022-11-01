namespace VappsWeb.Config
{
    public static class AppConfig
    {
        public struct LocalStoreItems
        {
            public const string AuthorizationToken = nameof(AuthorizationToken);
            public const string Culture = "culture";
        }

        public struct Colors
        {
            public const string Primary = "#0068c6";
            public const string Secondary = "#7dd4ff";
        }

        public struct ApiRoutes
        {
            public const string MasterService = @"https://api.systemywp.pl";
            public const string MasterServiceDEV = @"http://localhost:5000";
            public const string LoginPath = @"/auth/authenticate";
        }
    }
}
