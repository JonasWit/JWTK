using VappsMobile.ViewModels;

namespace VappsMobile.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterPageViewModel registerPageViewModel)
	{
		InitializeComponent();

		BindingContext = registerPageViewModel;
	}
}