using VappsMobile.RoutingServices;

namespace VappsMobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RouterService.Register();
        }
    }
}