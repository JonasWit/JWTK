using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;
using VappsWeb.Config;

namespace VappsWeb.Extensions
{
    public static class WebAssemblyHostExtension
    {
        public static async Task SetDefaultCulture(this WebAssemblyHost webAssemblyHost)
        {
            ILocalStorageService localStorage = webAssemblyHost.Services.GetRequiredService<ILocalStorageService>();
            var cultureFromStorage = await localStorage.GetItemAsync<string>(AppConfig.LocalStoreItems.Culture);

            CultureInfo culture;
            if (!string.IsNullOrEmpty(cultureFromStorage))
            {
                culture = new CultureInfo(cultureFromStorage);
            }
            else
            {
                culture = new CultureInfo("en-US");
            }

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
    }
}
