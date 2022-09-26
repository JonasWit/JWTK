using System.Collections.ObjectModel;
using VappsMobile.Models.GastroAppModels;

namespace VappsMobile.ViewModels.GastroApp
{
    public partial class IngredientsPageViewModel : ViewModelBase
    {
        public ObservableCollection<IngredientDto> IngredientDtos { get; } = new();

        public IngredientsPageViewModel()
        {

        }
    }
}
