using System.Collections.ObjectModel;
using VappsMobile.AppConfig;
using VappsMobile.Models.GeneralModels;

namespace VappsMobile.ViewModels
{
    public partial class VappsMasterPageViewModel : ViewModelBase
    {
        public ObservableCollection<Vapp> Vapps { get; } = new();

        public VappsMasterPageViewModel() => Vapps = new ObservableCollection<Vapp>(AppConstants.GetAvailableVapps());
    }
}
