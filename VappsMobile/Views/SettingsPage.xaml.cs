using VappsMobile.CustomAttributes;
using VappsMobile.ViewModels;

namespace VappsMobile.Views;

[ServiceRegistrationType(ServiceLifetime.Singleton)]
public partial class SettingsPage : ContentPage
{
	private readonly SettingsPageViewModel _settingsPageViewModel;

	public SettingsPage(SettingsPageViewModel settingsPageViewModel)
	{
		InitializeComponent();
		_settingsPageViewModel = settingsPageViewModel;
		BindingContext = _settingsPageViewModel;
	}
}