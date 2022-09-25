using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using VappsMobile.AppConfig;
using VappsMobile.Models.GeneralModels;
using VappsMobile.Views.GastroApp;

namespace VappsMobile.ViewModels.GastroApp
{
    public partial class GastroAppMasterPageViewModel : ViewModelBase
    {
        public ObservableCollection<DefaultMenuItem> MenuItems { get; } = new();

        public GastroAppMasterPageViewModel() => MenuItems = new ObservableCollection<DefaultMenuItem>(new List<DefaultMenuItem>
        {
            new DefaultMenuItem
            {
                Image = Icons.GastroAppMenu,
                Name = "Menu",
                Description = "Komponuj własne menu z wcześniej dodanych dań",
                PageRoute = nameof(MenusPage)
            },
            new DefaultMenuItem
            {
                Image = Icons.GastroAppDish,
                Name = "Dania",
                Description = "Stwórz dania z dodanych składników",
                PageRoute = nameof(DishesPage)
            },
            new DefaultMenuItem
            {
                Image = Icons.GastroAppIngredient,
                Name = "Składniki",
                Description = "Składniki potrzebne do tworzenia dań",
                PageRoute = nameof(IngredientsPage)
            },
        });

        [RelayCommand]
        public async Task GoTo(DefaultMenuItem mi) => await Shell.Current.GoToAsync(mi.PageRoute);
    }
}
