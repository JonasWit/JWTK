using VappsMobile.AppConfig;
using VappsMobile.ViewModels;

namespace VappsMobile
{
    public partial class AppShell : Shell
    {
        public AppShell(ShellViewModel shellViewModel)
        {
            InitializeComponent();
            BindingContext = shellViewModel;
            RouterService.Register();
        }
    }
}