using VappsMobile.Services;
using VappsMobile.ViewModels;

namespace VappsMobile.Views;

public partial class RegisterPage : ContentPage
{
	private readonly LoginPageViewModel _loginPageViewModel;
	private readonly AuthService _authService;

	public RegisterPage(LoginPageViewModel loginPageViewModel, AuthService authService)
	{
		InitializeComponent();
		_loginPageViewModel = loginPageViewModel;
		_authService = authService;
	}
}