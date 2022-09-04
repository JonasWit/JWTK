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
            var builder = MauiApp.CreateBuilder();

            builder.Services.AddSingleton(new HttpClientPolicy());

            builder.Services.AddHttpClient<VappsHttpClient>(httpClient =>
                httpClient.BaseAddress = new Uri(AppConstants.BaseUrls.MasterUrl));


            builder.Services.AddSingleton<MainPageVM>();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            App = builder.Build();
            return App;
        }
    }
}