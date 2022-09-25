using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using VappsMobile.AppConfig;
using VappsMobile.Models.GeneralModels;
using VappsMobile.Views.GastroApp;

namespace VappsMobile.ViewModels
{
    public partial class VappsMasterPageViewModel : ViewModelBase
    {
        public ObservableCollection<Vapp> Vapps { get; } = new();

        public VappsMasterPageViewModel() => Vapps = new ObservableCollection<Vapp>(new List<Vapp>()
        {
            new Vapp { Name = "Gastro-Apka", Description = "opis", Image = Icons.GastroAppDefault, MasterPageRoute = nameof(GastroAppMasterPage)  }
        });

        [RelayCommand]
        public async Task GoToApp(Vapp vapp) => await Shell.Current.GoToAsync(vapp.MasterPageRoute);
    }
}
