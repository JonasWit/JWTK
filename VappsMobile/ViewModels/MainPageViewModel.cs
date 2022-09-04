namespace VappsMobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string _mainTitle;
        public string MainTitle { get => _mainTitle; set { _mainTitle = value; OnPropertyChanged(); } }

        public MainPageViewModel()
        {
            MainTitle = "test main title from VM";
        }
    }
}
