using VappsMobile.ViewModels.GastroApp;

namespace VappsMobile.Views.GastroApp;

public partial class GastroAppMasterPage : ContentPage
{
	public GastroAppMasterPage(GastroAppMasterPageViewModel gastroAppMasterPageViewModel)
	{
		InitializeComponent();
		BindingContext = gastroAppMasterPageViewModel;
	}
}