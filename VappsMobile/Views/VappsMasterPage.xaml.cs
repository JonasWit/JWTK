using VappsMobile.ViewModels;

namespace VappsMobile.Views;

public partial class VappsMasterPage : ContentPage
{
	private readonly VappsMasterPageViewModel _vappsMasterPageViewModel;

	public VappsMasterPage(VappsMasterPageViewModel vappsMasterPageViewModel)
	{
		InitializeComponent();

		_vappsMasterPageViewModel = vappsMasterPageViewModel;

		BindingContext = _vappsMasterPageViewModel;
	}
}