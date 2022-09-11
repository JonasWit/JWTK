using CommunityToolkit.Mvvm.ComponentModel;
using VappsMobile.Models.GeneralModels;

namespace VappsMobile.ViewModels
{
    [QueryProperty(nameof(ErrorModalContent), nameof(ErrorModalContent))]
    public partial class ErrorModalPageViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ErrorModalContent _errorModalContent;

        public ErrorModalPageViewModel()
        {

        }
    }
}
