using CommunityToolkit.Mvvm.Input;
using VappsMobile.Services;
using VappsMobile.Views;

namespace VappsMobile.ViewModels
{
    public partial class ShellViewModel : ViewModelBase
    {
        private readonly AuthService _authService;

        public ShellViewModel(AuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        private async void SignOut()
        {
            try
            {
                _ = _authService.SignOut();
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            catch (Exception)
            {

            }
        }
    }
}
