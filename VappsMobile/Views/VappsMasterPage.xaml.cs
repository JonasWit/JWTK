using VappsMobile.CustomAttributes;
using VappsMobile.ViewModels;

namespace VappsMobile.Views;

[ServiceRegistrationType(ServiceLifetime.Singleton)]
public partial class VappsMasterPage : ContentPage
{
	public VappsMasterPage(VappsMasterPageViewModel vappsMasterPageViewModel)
	{
		InitializeComponent();
		BindingContext = vappsMasterPageViewModel;
	}
}