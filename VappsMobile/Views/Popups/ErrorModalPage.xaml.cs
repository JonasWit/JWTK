using VappsMobile.AppConfig;
using VappsMobile.ViewModels;

namespace VappsMobile.Views.Popups;

public partial class ErrorModalPage : ContentPage
{
	private readonly ErrorModalPageViewModel _errorModalPage;

	public ErrorModalPage(ErrorModalPageViewModel errorModalPage)
	{
		InitializeComponent();

		_errorModalPage = errorModalPage;

		BindingContext = _errorModalPage;
	}

	private async void Button_Clicked(object sender, EventArgs e) => await Shell.Current.GoToAsync(AppConstants.Navigation.PopCurrent);
}