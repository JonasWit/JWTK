using VappsMobile.CustomAttributes;
using VappsMobile.ViewModels;

namespace VappsMobile.Views.Popups;

[ServiceRegistrationType(ServiceLifetime.Transient)]
public partial class DefaultModalPage : ContentPage
{
	public DefaultModalPage(DefaultModalPageViewModel defaultModalPageViewModel)
	{
		InitializeComponent();
		BindingContext = defaultModalPageViewModel;
	}
}