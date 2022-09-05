using VappsMobile.Pages;
using VappsMobile.Services;

namespace VappsMobile
{
    public partial class App : Application
    {
        public App(AuthService authService, AppShell appShell, LoginPage loginPage)
        {
            InitializeComponent();
            MainPage = authService.IsSignedIn() ? appShell : loginPage;
        }
    }
}