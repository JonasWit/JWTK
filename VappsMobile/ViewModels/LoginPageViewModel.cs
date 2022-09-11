using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VappsMobile.AppConfig;
using VappsMobile.CustomControls;
using VappsMobile.Services;
using VappsMobile.Views;

namespace VappsMobile.ViewModels
{
    public partial class LoginPageViewModel : ViewModelBase
    {
        private readonly AuthService _authService;
        private readonly HealthService _healthService;

        //[ObservableProperty]
        //private string _validationMessage;

        //[ObservableProperty]
        //private bool _validationVisible;

        [ObservableProperty]
        private bool _rememberMe;

        [ObservableProperty]

        private string _email;

        [ObservableProperty]
        private string _password;

        public LoginPageViewModel(AuthService authService, HealthService healthService)
        {
            _authService = authService;
            _healthService = healthService;
            RememberMe = true;
        }

        //partial void OnEmailChanging(string value)
        //{
        //    if (string.IsNullOrEmpty(value))
        //    {
        //        _validationVisible = false;
        //        _validationMessage = string.Empty;
        //        return;
        //    }

        //    _validationVisible = true;
        //    _validationMessage = value;
        //}

        [RelayCommand]
        public async void SignIn()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(LoadingPage));

                if (await _authService.SignIn(_email, _password, _rememberMe))
                {
                    Shell.Current.FlyoutHeader = new FlyoutHeader(_authService.UserInfo.Email);
                    await Shell.Current.GoToAsync($"//{nameof(VappsMasterPage)}");
                    return;
                }

                await Shell.Current.GoToAsync(AppConstants.Navigation.PopCurrent);
            }
            catch (Exception)
            {
                await Shell.Current.GoToAsync(AppConstants.Navigation.PopCurrent);
            }
        }

        [RelayCommand]
        public void ForgotPassword()
        {

        }

        [RelayCommand]
        public void SignUp()
        {
            //if (!string.IsNullOrEmpty(_email) && !string.IsNullOrEmpty(_password))
            //{
            //    if (await _authService.SignIn(_email, _password))
            //    {

            //    }

            //    if (Preferences.ContainsKey(nameof(UserInfo)))
            //    {
            //        Preferences.Remove(nameof(UserInfo));
            //    }

            //    var userInfo = JsonSerializer.Serialize(user);
            //    Preferences.Set(nameof(UserInfo), userInfo);

            //}
        }
    }
}
