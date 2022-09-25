namespace VappsMobile.Views.Popups;

public partial class DefaultModalPage : ContentPage
{
	private readonly DefaultModalPage _defaultModalPage;

	public DefaultModalPage(DefaultModalPage defaultModalPage)
	{
		InitializeComponent();
		_defaultModalPage = defaultModalPage;
		BindingContext = _defaultModalPage;
	}
}