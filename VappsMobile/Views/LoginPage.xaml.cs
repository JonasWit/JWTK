using VappsMobile.CustomAttributes;
using VappsMobile.Services;
using VappsMobile.ViewModels;

namespace VappsMobile.Views;

[ServiceRegistrationType(ServiceLifetime.Transient)]
public partial class LoginPage : ContentPage
{
	private readonly LoginPageViewModel _loginPageViewModel;
	private readonly AuthService _authService;

	public LoginPage(LoginPageViewModel loginPageViewModel, AuthService authService)
	{
		InitializeComponent();
		_loginPageViewModel = loginPageViewModel;
		_authService = authService;
		BindingContext = _loginPageViewModel;
	}

	protected override async void OnAppearing()
	{
		_loginPageViewModel.IsBusy = true;

		if (await _authService.GetStoredUser())
		{
			await Shell.Current.GoToAsync($"//{nameof(VappsMasterPage)}");
			_loginPageViewModel.IsBusy = false;
			return;
		}

		_loginPageViewModel.IsBusy = false;
		base.OnAppearing();
	}
}