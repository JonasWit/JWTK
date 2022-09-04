using VappsMobile.Policies;
using VappsMobile.Services;
using VappsMobile.ViewModels;

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

            _ = builder.Services.AddSingleton<MainPageViewModel>();

            _ = builder.Services.AddSingleton<AppShell>();
            _ = builder.Services.AddSingleton<MainPage>();
            _ = builder.Services.AddSingleton<MainPageViewModel>();

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