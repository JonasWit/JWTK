using CommunityToolkit.Mvvm.ComponentModel;

namespace BlazorWASMSandbox.ViewModels
{
    public partial class NumberViewModel : ViewModelBase
    {
        [ObservableProperty]

        private int _countValue;
    }
}
