using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VappsMobile.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        public string mainTitle;

        [ObservableProperty]
        public string password;

        public MainPageViewModel()
        {
            MainTitle = "test main title from VM";
        }

        [RelayCommand]
        public Task Validate()
        {
            return Task.CompletedTask;
        }
    }
}
