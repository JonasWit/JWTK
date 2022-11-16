using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using VappsWeb.Config;

namespace VappsWeb.Components.Localization
{
    public partial class CultureDropdown
    {
        private readonly CultureInfo[] _cultures = new[] { new CultureInfo(AppConfig.CultureKeys.EN), new CultureInfo(AppConfig.CultureKeys.PL), };

        [Inject]
        public NavigationManager NavManager { get; set; } = null!;

        [Inject]
        public ILocalStorageService LocalStorage { get; set; } = null!;

        private CultureInfo Culture
        {
            get => CultureInfo.CurrentCulture;
            set
            {
                if (CultureInfo.CurrentCulture != value)
                {
                    _ = LocalStorage.SetItemAsync(AppConfig.LocalStoreItems.Culture, value.Name);
                    NavManager.NavigateTo(NavManager.Uri, forceLoad: true);
                }
            }
        }
    }
}