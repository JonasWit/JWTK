namespace VappsMobile.ViewModels
{
    internal class MainPageVM : ViewModelBase
    {
        private string _mainTitle;
        public string MainTitle { get => _mainTitle; set { _mainTitle = value; OnPropertyChanged(); } }

        public MainPageVM()
        {
            MainTitle = "test main title";
        }
    }
}
