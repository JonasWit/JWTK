using MudBlazor;

namespace VappsWeb.Shared
{
    public partial class MainLayout
    {
        private readonly MudTheme _theme = new()
        {
            Palette = new Palette()
            {
                Primary = Colors.Blue.Darken2,
                PrimaryLighten = Colors.Blue.Lighten2,
                PrimaryDarken = Colors.Blue.Darken4,
                AppbarBackground = Colors.Blue.Darken2,
                TextPrimary = Colors.Grey.Darken4,
            },
            PaletteDark = new PaletteDark()
            {
                PrimaryLighten = Colors.Blue.Lighten2,
                PrimaryDarken = Colors.Blue.Darken4,
                TextPrimary = Colors.Grey.Lighten5,
            },
        };

        private bool _isDarkMode;
        private bool _drawerOpen = true;

        private void DrawerToggle() => _drawerOpen = !_drawerOpen;
    }
}