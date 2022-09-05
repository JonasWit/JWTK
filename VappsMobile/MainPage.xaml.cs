using VappsMobile.Services;
using VappsMobile.ViewModels;

namespace VappsMobile
{
    public partial class MainPage : ContentPage
    {
        private readonly AuthService _authService;

        public MainPage(MainPageViewModel mainPageViewModel, AuthService authService)
        {
            InitializeComponent();
            BindingContext = mainPageViewModel;
            _authService = authService;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
    }
}