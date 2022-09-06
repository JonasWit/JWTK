using CommunityToolkit.Mvvm.Input;
using VappsMobile.Models;
using VappsMobile.Views;

namespace VappsMobile.ViewModels
{
    public partial class ShellViewModel : ViewModelBase
    {

        [RelayCommand]
        private async void SignOut()
        {

            if (Preferences.ContainsKey(nameof(UserInfo)))
            {
                Preferences.Remove(nameof(UserInfo));
            }

            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
