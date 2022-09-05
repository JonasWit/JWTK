using CommunityToolkit.Mvvm.ComponentModel;
using VappsMobile.Policies;
using VappsMobile.Services;

namespace VappsMobile
{
    public static class MauiProgram
    {
        public static MauiApp App;

        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();

            _ = builder.Services.AddSingleton(new HttpClientPolicy());

            _ = builder.Services.AddHttpClient<VappsHttpClient>(httpClient =>
                httpClient.BaseAddress = new Uri(AppConstants.BaseUrls.MasterUrl));

            IEnumerable<System.Reflection.TypeInfo> appDefinedTypes = typeof(MauiProgram).Assembly.DefinedTypes;

            var viewModels = appDefinedTypes
                .Where(t => t.Name.Contains("ViewModel") && !t.Name.Contains("Base") && t.IsSubclassOf(typeof(ObservableObject)))
                .Select(t => t.AsType())
                .ToList();

            viewModels.ForEach(dt => builder.Services.AddSingleton(dt));

            _ = builder.Services.AddSingleton<AppShell>();
            _ = builder.Services.AddSingleton<MainPage>();

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