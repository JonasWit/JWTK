using VappsMobile.Pages;

namespace VappsMobile.RoutingServices
{
    public static class RouterService
    {
        public static void Register()
        {
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(VappsMasterPage), typeof(VappsMasterPage));
        }
    }
}
