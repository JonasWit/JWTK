using VappsMobile.ViewModels;

namespace VappsMobile.Views;

public partial class VappsMasterPage : ContentPage
{
	public VappsMasterPage(VappsMasterPageViewModel vappsMasterPageViewModel)
	{
		InitializeComponent();
		BindingContext = vappsMasterPageViewModel;
	}
}