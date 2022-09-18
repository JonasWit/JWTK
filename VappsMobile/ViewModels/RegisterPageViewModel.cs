using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VappsMobile.Models.GeneralModels;
using VappsMobile.Services;
using VappsMobile.Views.Popups;

namespace VappsMobile.ViewModels
{
    public partial class RegisterPageViewModel : ViewModelBase
    {
        private readonly AuthService _authService;

        [ObservableProperty]

        private string _email;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _test;

        [ObservableProperty]
        private string _confirmPassword;

        public RegisterPageViewModel(AuthService authService)
        {
            _test = "TEST";
            _authService = authService;
        }

        [RelayCommand]
        public async Task SignUp()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;

            try
            {

            }
            catch (Exception ex)
            {
                await Shell.Current.GoToAsync(nameof(ErrorModalPage), true, new Dictionary<string, object>
                {
                    {"ErrorModalContent", new ErrorModalContent{ Message = "Logowanie nie powiodło się", Exception = ex} }
                });
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
