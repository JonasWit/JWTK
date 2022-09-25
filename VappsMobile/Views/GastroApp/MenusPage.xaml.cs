using VappsMobile.CustomAttributes;
using VappsMobile.ViewModels.GastroApp;

namespace VappsMobile.Views.GastroApp;

[ServiceRegistrationType(ServiceLifetime.Singleton)]
public partial class MenusPage : ContentPage
{
	public MenusPage(MenusPageViewModel menusPageViewModel)
	{
		InitializeComponent();
		BindingContext = menusPageViewModel;
	}
}