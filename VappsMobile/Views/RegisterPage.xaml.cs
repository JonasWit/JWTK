using VappsMobile.CustomAttributes;
using VappsMobile.ViewModels;

namespace VappsMobile.Views;

[ServiceRegistrationType(ServiceLifetime.Transient)]
public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterPageViewModel registerPageViewModel)
	{
		InitializeComponent();

		BindingContext = registerPageViewModel;
	}
}