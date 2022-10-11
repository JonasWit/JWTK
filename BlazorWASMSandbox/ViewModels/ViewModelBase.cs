using CommunityToolkit.Mvvm.ComponentModel;

namespace BlazorWASMSandbox.ViewModels
{
    public partial class ViewModelBase : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        public bool _isBusy;

        [ObservableProperty]
        public string? _title;

        public bool IsNotBusy => !IsBusy;
    }
}
