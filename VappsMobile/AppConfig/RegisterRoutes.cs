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
        }
    }
}
