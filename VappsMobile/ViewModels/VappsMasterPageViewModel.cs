using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using VappsMobile.AppConfig;
using VappsMobile.Models.GeneralModels;

namespace VappsMobile.ViewModels
{
    public partial class VappsMasterPageViewModel : ViewModelBase
    {
        public ObservableCollection<Vapp> Vapps { get; } = new();

        public VappsMasterPageViewModel() => Vapps = new ObservableCollection<Vapp>(AppConstants.GetAvailableVapps());

        [RelayCommand]
        public async Task GoToApp(Vapp vapp) => await Shell.Current.GoToAsync(vapp.MasterPageRoute);
    }
}
