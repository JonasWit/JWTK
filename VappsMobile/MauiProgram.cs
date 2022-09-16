using System.Net.Http.Headers;
using VappsMobile.AppConfig;
using VappsMobile.CustomControls;
using VappsMobile.Policies;
using VappsMobile.Services;
using VappsMobile.ViewModels;
using VappsMobile.Views;
using VappsMobile.Views.Popups;

namespace VappsMobile
{
    public static class MauiProgram
    {
        public static MauiApp App;

        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();

            _ = builder.Services.AddSingleton(new HttpClientPolicy());

#if DEBUG
            var url = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
#else
            var url = ApiConfig.ApiUrls.MasterService;
#endif
            _ = builder.Services.AddHttpClient<VappsHttpClient>(
            ApiConfig.HttpClientsNames.AuthClient,
            (provider, httpClient) =>
            {
                httpClient.BaseAddress = new Uri($"{url}/{ApiConfig.ApiAuthController.BasePath}/");
                httpClient.Timeout = TimeSpan.FromSeconds(15);

                var token = provider.GetService<UserService>().UserInfo?.Token;
                if (!string.IsNullOrEmpty(token))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", provider.GetService<UserService>().UserInfo?.Token);
                }
            });
            _ = builder.Services.AddHttpClient<VappsHttpClient>(
            ApiConfig.HttpClientsNames.HealthClient,
            httpClient => { httpClient.BaseAddress = new Uri($"{url}/{ApiConfig.ApiHealthController.BasePath}/"); httpClient.Timeout = TimeSpan.FromSeconds(15); });

            _ = builder.Services.AddSingleton<AppShell>();
            _ = builder.Services.AddSingleton<AuthService>();
            _ = builder.Services.AddSingleton<HealthService>();
            _ = builder.Services.AddSingleton<FlyoutHeader>();
            _ = builder.Services.AddSingleton<UserService>();

            _ = builder.Services.AddTransient<IntroPage>();
            _ = builder.Services.AddTransient<LoginPageViewModel>();
            _ = builder.Services.AddTransient<ErrorModalPageViewModel>();
            _ = builder.Services.AddSingleton<MainPageViewModel>();
            _ = builder.Services.AddSingleton<ShellViewModel>();
            _ = builder.Services.AddSingleton<VappsMasterPageViewModel>();
            _ = builder.Services.AddSingleton<SettingsPageViewModel>();

            _ = builder.Services.AddTransient<LoginPage>();
            _ = builder.Services.AddTransient<ErrorModalPage>();
            _ = builder.Services.AddSingleton<RegisterPage>();
            _ = builder.Services.AddSingleton<VappsMasterPage>();
            _ = builder.Services.AddSingleton<SettingsPage>();

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