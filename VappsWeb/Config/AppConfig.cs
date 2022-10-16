namespace VappsWeb.Config
{
    public static class AppConfig
    {
        public struct LocalStoreItems
        {
            public const string AuthorizationToken = "jwt_auth";
        }

        public struct ApiRoutes
        {
            public const string MasterService = @"https://api.systemywp.pl";
            public const string MasterServiceDEV = @"http://localhost:5000";
            public const string LoginPath = @"/auth/authenticate";
        }
    }
}
