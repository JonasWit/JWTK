using CommunityToolkit.Mvvm.ComponentModel;
using VappsMobile.Services;

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
        private string _confirmPassword;

        public RegisterPageViewModel(AuthService authService) => _authService = authService;

    }
}
