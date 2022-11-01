using MudBlazor;
using VappsWeb.Config;

namespace VappsWeb.Shared
{
    public partial class MainLayout
    {
        private readonly MudTheme _theme = new()
        {
            Palette = new Palette()
            {
                Primary = AppConfig.Colors.Primary,
                Secondary = AppConfig.Colors.Secondary,
                AppbarBackground = AppConfig.Colors.Primary,
            },
        };

        private bool _isDarkMode;
        private bool _drawerOpen = true;

        private void DrawerToggle() => _drawerOpen = !_drawerOpen;
    }
}