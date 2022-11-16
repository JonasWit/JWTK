namespace VappsWeb.Config
{
    public static class AppConfig
    {
        public struct LocalStoreItems
        {
            public const string AuthorizationToken = nameof(AuthorizationToken);
            public const string Culture = "culture";
        }

        public struct ApiRoutes
        {
            public const string MasterService = @"https://api.systemywp.pl";
            public const string MasterServiceDEV = @"http://localhost:5000";
            public const string LoginPath = @"/auth/authenticate";
        }

        public struct CultureKeys
        {
            public const string EN = "en-US";
            public const string PL = "pl-PL";
            public const string ENISO = "en";
            public const string PLISO = "pl";
        }
    }
}
