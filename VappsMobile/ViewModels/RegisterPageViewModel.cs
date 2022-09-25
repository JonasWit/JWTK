using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VappsMobile.AppConfig;
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
        private string _confirmPassword;

        public RegisterPageViewModel(AuthService authService) => _authService = authService;

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
                if (await _authService.SignUp(_email, _password))
                {
                    await Shell.Current.GoToAsync($"{nameof(DefaultModalPage)}", true, new Dictionary<string, object>
                    {
                        {
                            "DefaultModalContent", new DefaultModalContent
                            {
                                Message = "Rejestracja powiodła się, potwierdź rejestracjęklikając link w otrzymanym mailu",
                                Icon = Icons.Done,
                                OnClickAction = () => Shell.Current.GoToAsync("../..")
                            }
                        }
                    });
                    return;
                }

                throw new Exception();
            }
            catch (Exception)
            {
                await Shell.Current.GoToAsync(nameof(DefaultModalPage), true, new Dictionary<string, object>
                {
                    {
                        "DefaultModalContent", new DefaultModalContent
                        {
                            Message = "Rejestracja nie powiodła się, sprawdź połączenie z internetem",
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
    }
}
