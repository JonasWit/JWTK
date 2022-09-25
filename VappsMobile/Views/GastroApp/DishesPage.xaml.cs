using VappsMobile.CustomAttributes;
using VappsMobile.ViewModels.GastroApp;

namespace VappsMobile.Views.GastroApp;

[ServiceRegistrationType(ServiceLifetime.Singleton)]
public partial class DishesPage : ContentPage
{
	public DishesPage(DishesPageViewModel dishesPageViewModel)
	{
		InitializeComponent();
		BindingContext = dishesPageViewModel;
	}
}