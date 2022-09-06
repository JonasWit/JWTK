using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VappsMobile.ViewModels
{
    public partial class LoginPageViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        public LoginPageViewModel()
        {

        }

        [RelayCommand]
        public async void Login()
        {

        }
    }
}
