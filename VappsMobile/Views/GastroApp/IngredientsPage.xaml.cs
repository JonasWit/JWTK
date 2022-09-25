using VappsMobile.CustomAttributes;
using VappsMobile.ViewModels.GastroApp;

namespace VappsMobile.Views.GastroApp;

[ServiceRegistrationType(ServiceLifetime.Singleton)]
public partial class IngredientsPage : ContentPage
{
	public IngredientsPage(IngredientsPageViewModel ingredientsPageViewModel)
	{
		InitializeComponent();
		BindingContext = ingredientsPageViewModel;
	}
}