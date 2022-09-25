using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VappsMobile.Models.GeneralModels;

namespace VappsMobile.ViewModels
{
    [QueryProperty(nameof(DefaultModalContent), nameof(DefaultModalContent))]
    public partial class DefaultModalPageViewModel : ViewModelBase
    {
        [ObservableProperty]
        private DefaultModalContent _defaultModalContent;

        public DefaultModalPageViewModel()
        {

        }

        [RelayCommand]
        public async Task PressButton() => await _defaultModalContent.OnClickAction.Invoke();
    }
}
