using VappsMobile.Services;

namespace VappsMobile.Views;

public partial class IntroPage : ContentPage
{
    private readonly AuthService _authService;

    public IntroPage(AuthService authService)
    {
        InitializeComponent();
        _authService = authService;
    }

    protected override async void OnAppearing()
    {
        if (await _authService.GetStoredUser())
        {
            await Shell.Current.GoToAsync($"{nameof(VappsMasterPage)}");
        }
        else
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }

        base.OnAppearing();
    }
}