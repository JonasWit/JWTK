using System.Reflection;

namespace VappsMobile.AppConfig
{
    public static class RouterService
    {
        public static void Register()
        {
            IEnumerable<TypeInfo> appDefinedTypes = typeof(MauiProgram).Assembly.DefinedTypes;

            var pages = appDefinedTypes
                .Where(t => t.Name.Contains("Page") && !t.Name.Contains("Base") && t.IsSubclassOf(typeof(ContentPage)))
                .Select(t => t.AsType())
                .ToList();

            foreach (Type page in pages)
            {
                Routing.RegisterRoute(page.Name, page);
            }

            //Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            //Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            //Routing.RegisterRoute(nameof(VappsMasterPage), typeof(VappsMasterPage));
            //Routing.RegisterRoute(nameof(ContactPage), typeof(ContactPage));
            //Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
            //Routing.RegisterRoute(nameof(LoadingPage), typeof(LoadingPage));
        }
    }
}
