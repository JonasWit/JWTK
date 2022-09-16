using CommunityToolkit.Mvvm.ComponentModel;

namespace VappsMobile.ViewModels
{
    public partial class ViewModelBase : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        public bool _isBusy;

        [ObservableProperty]
        public string _title;

        public bool IsNotBusy => !IsBusy;
    }
}
