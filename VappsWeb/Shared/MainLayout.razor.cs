using MudBlazor;

namespace VappsWeb.Shared
{
    public partial class MainLayout
    {
        private readonly MudTheme _theme = new();
        private bool _isDarkMode;
        private bool _drawerOpen = true;

        private void DrawerToggle() => _drawerOpen = !_drawerOpen;
    }
}