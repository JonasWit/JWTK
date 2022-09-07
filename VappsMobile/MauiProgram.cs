using VappsMobile.AppConfig;
using VappsMobile.Policies;
using VappsMobile.Services;
using VappsMobile.ViewModels;
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

            //_ = builder.Services.AddHttpClient<VappsHttpClient>(
            //    AppConstants.HttpClientsNames.AuthHttpClient,
            //    httpClient => httpClient.BaseAddress = new Uri(AppConstants.BaseUrls.MasterUrl));

            _ = builder.Services.AddHttpClient<VappsHttpClient>(AppConstants.HttpClientsNames.AuthHttpClient);

            IEnumerable<System.Reflection.TypeInfo> appDefinedTypes = typeof(MauiProgram).Assembly.DefinedTypes;

            _ = builder.Services.AddSingleton<AppShell>();
            _ = builder.Services.AddSingleton<AuthService>();

            _ = builder.Services.AddTransient<LoginPageViewModel>();
            _ = builder.Services.AddSingleton<MainPageViewModel>();
            _ = builder.Services.AddSingleton<ShellViewModel>();
            _ = builder.Services.AddSingleton<VappsMasterPageViewModel>();

            _ = builder.Services.AddTransient<LoginPage>();
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