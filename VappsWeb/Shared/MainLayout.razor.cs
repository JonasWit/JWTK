using MudBlazor;

namespace VappsWeb.Shared
{
    public partial class MainLayout
    {
        private readonly MudTheme _theme = new()
        {
            Palette = new Palette()
            {
                Primary = Colors.Blue.Darken4,
                Secondary = Colors.Indigo.Darken4,
                AppbarBackground = Colors.Blue.Darken4,
            },
        };

        private bool _isDarkMode;
        private bool _drawerOpen = true;

        private void DrawerToggle() => _drawerOpen = !_drawerOpen;
    }
}