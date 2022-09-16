using CommunityToolkit.Mvvm.Input;
using VappsMobile.Services;

namespace VappsMobile.ViewModels
{
    public partial class SettingsPageViewModel : ViewModelBase
    {
        private readonly AuthService _authService;

        public SettingsPageViewModel(AuthService authService) => _authService = authService;

        [RelayCommand]
        public async Task DelteAccount()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;

            try
            {
                var result = await _authService.DeleteAccount();
            }
            catch (Exception)
            {
                return;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
