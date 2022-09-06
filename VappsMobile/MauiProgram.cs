using CommunityToolkit.Mvvm.ComponentModel;
using VappsMobile.AppConfig;
using VappsMobile.Policies;
using VappsMobile.Services;
using VappsMobile.Views;

namespace VappsMobile
{
    public static class MauiProgram
    {
        public static MauiApp App;

        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();

            _ = builder.Services.AddSingleton(new HttpClientPolicy());

            _ = builder.Services.AddHttpClient<VappsHttpClient>(
                AppConstants.HttpClientsNames.AuthHttpClient,
                httpClient => httpClient.BaseAddress = new Uri(AppConstants.BaseUrls.MasterUrl));

            IEnumerable<System.Reflection.TypeInfo> appDefinedTypes = typeof(MauiProgram).Assembly.DefinedTypes;

            var viewModels = appDefinedTypes
                .Where(t => t.Name.Contains("ViewModel") && !t.Name.Contains("Base") && t.IsSubclassOf(typeof(ObservableObject)))
                .Select(t => t.AsType())
                .ToList();

            viewModels.ForEach(vm => builder.Services.AddSingleton(vm));

            _ = builder.Services.AddSingleton<AppShell>();
            _ = builder.Services.AddSingleton<AuthService>();

            _ = builder.Services.AddSingleton<LoginPage>();
            _ = builder.Services.AddSingleton<RegisterPage>();
            _ = builder.Services.AddSingleton<VappsMasterPage>();

            _ = builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    _ = fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    _ = fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            App = builder.Build();
            return App;
        }
    }
}