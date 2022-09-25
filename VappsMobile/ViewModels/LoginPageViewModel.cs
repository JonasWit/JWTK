using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VappsMobile.AppConfig;
using VappsMobile.CustomControls;
using VappsMobile.Models.GeneralModels;
using VappsMobile.Services;
using VappsMobile.Views;
using VappsMobile.Views.Popups;

namespace VappsMobile.ViewModels
{
    public partial class LoginPageViewModel : ViewModelBase
    {
        private readonly AuthService _authService;
        private readonly HealthService _healthService;
        private readonly UserService _userService;
        [ObservableProperty]
        private bool _rememberMe;

        [ObservableProperty]

        private string _email;

        [ObservableProperty]
        private string _password;

        public LoginPageViewModel(AuthService authService, HealthService healthService, UserService userService)
        {
            _authService = authService;
            _healthService = healthService;
            _userService = userService;
            RememberMe = true;
        }

        [RelayCommand]
        public async Task SignIn()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                if (await _authService.SignIn(_email, _password, _rememberMe))
                {
                    Shell.Current.FlyoutHeader = new FlyoutHeader(_userService.UserInfo.Email);
                    await Shell.Current.GoToAsync($"//{nameof(VappsMasterPage)}");
                    return;
                }

                throw new Exception();
            }
            catch (Exception)
            {
                await Shell.Current.GoToAsync(nameof(DefaultModalPage), true, new Dictionary<string, object>
                {
                    {
                        "ErrorModalContent", new DefaultModalContent
                        {
                            Message = "Logowanie nie powiodło się",
                            Icon = Icons.Error,
                            OnClickAction = () => Shell.Current.GoToAsync("..")
                        }
                    }
                });
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task ForgotPassword()
        {
            if (IsBusy)
            {
                return;
            }
            await Shell.Current.GoToAsync($"{nameof(ForgotPasswordPage)}");
        }

        [RelayCommand]
        public async Task SignUp()
        {
            if (IsBusy)
            {
                return;
            }
            await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
        }
    }
}
