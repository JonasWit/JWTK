using VappsMobile.Services;
using VappsMobile.ViewModels;

namespace VappsMobile.Views;

public partial class LoginPage : ContentPage
{
	private readonly AuthService _authService;

	public LoginPage(LoginPageViewModel loginPageViewModel, AuthService authService)
	{
		InitializeComponent();
		_authService = authService;
		BindingContext = loginPageViewModel;
	}

	protected override async void OnAppearing()
	{
		if (await _authService.GetStoredUser())
		{
			await Shell.Current.GoToAsync($"//{nameof(VappsMasterPage)}");
			return;
		}

		base.OnAppearing();
	}
}