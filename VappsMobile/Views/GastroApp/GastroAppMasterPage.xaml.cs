using VappsMobile.CustomAttributes;
using VappsMobile.ViewModels.GastroApp;

namespace VappsMobile.Views.GastroApp;

[ServiceRegistrationType(ServiceLifetime.Singleton)]
public partial class GastroAppMasterPage : ContentPage
{
	public GastroAppMasterPage(GastroAppMasterPageViewModel gastroAppMasterPageViewModel)
	{
		InitializeComponent();
		BindingContext = gastroAppMasterPageViewModel;
	}
}