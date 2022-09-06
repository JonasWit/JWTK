using VappsMobile.Models;

namespace VappsMobile
{
    public partial class App : Application
    {
        public static UserInfo UserInfo;

        public App(AppShell appShell)
        {
            InitializeComponent();
            MainPage = appShell;
        }
    }
}