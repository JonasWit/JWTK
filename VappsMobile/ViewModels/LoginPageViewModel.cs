using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json;
using VappsMobile.AppConfig;
using VappsMobile.CustomControls;
using VappsMobile.Models;
using VappsMobile.Services;
using VappsMobile.Views;

namespace VappsMobile.ViewModels
{
    public partial class LoginPageViewModel : ViewModelBase
    {
        private readonly AuthService _authService;
        private readonly HealthService _healthService;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        public LoginPageViewModel(AuthService authService, HealthService healthService)
        {
            _authService = authService;
            _healthService = healthService;
        }

        [RelayCommand]
        public async void SignIn()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(LoadingPage));
                _ = await _healthService.CheckHealth();
            }
            catch (Exception)
            {
            }
            finally
            {
                await Shell.Current.GoToAsync(AppConstants.Navigation.PopCurrent);
            }
        }

        [RelayCommand]
        public async void Login()
        {
            if (!string.IsNullOrEmpty(_email) && !string.IsNullOrEmpty(_password))
            {
                UserInfo user = await _authService.SignIn(_email, _password);

                if (Preferences.ContainsKey(nameof(UserInfo)))
                {
                    Preferences.Remove(nameof(UserInfo));
                }

                var userInfo = JsonSerializer.Serialize(user);
                Preferences.Set(nameof(UserInfo), userInfo);
                App.UserInfo = user;

                Shell.Current.FlyoutHeader = new FlyoutHeader();

                await Shell.Current.GoToAsync($"//{nameof(VappsMasterPage)}");
            }
        }
    }
}
