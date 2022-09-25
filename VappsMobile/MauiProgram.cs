using System.Net.Http.Headers;
using System.Reflection;
using VappsMobile.AppConfig;
using VappsMobile.CustomAttributes;
using VappsMobile.CustomControls;
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

            IEnumerable<TypeInfo> appDefinedTypes = typeof(MauiProgram).Assembly.DefinedTypes;

            var pages = appDefinedTypes
                   .Where(t => t.Name.Contains("Page") && !t.Name.Contains("Base") && t.IsSubclassOf(typeof(ContentPage)) && t.GetCustomAttribute(typeof(ServiceRegistrationType)) != null)
                   .Select(t => t.AsType())
                   .ToList();

            var viewModels = appDefinedTypes
                   .Where(t => t.Name.Contains("ViewModel") && !t.Name.Contains("Base") && t.IsSubclassOf(typeof(ViewModelBase)))
                   .Select(t => t.AsType())
                   .ToList();

            foreach (Type page in pages)
            {
                var registrationType = page.GetCustomAttribute(typeof(ServiceRegistrationType)) as ServiceRegistrationType;
                Type vm = viewModels.FirstOrDefault(vm => vm.Name.Equals($"{page.Name}ViewModel"));

                switch (registrationType.Lifetime)
                {
                    case ServiceLifetime.Singleton:
                        _ = builder.Services.AddSingleton(page);
                        if (vm is not null)
                        {
                            _ = builder.Services.AddSingleton(vm);
                        }

                        break;
                    case ServiceLifetime.Scoped:
                        _ = builder.Services.AddScoped(page);
                        if (vm is not null)
                        {
                            _ = builder.Services.AddScoped(vm);
                        }

                        break;
                    case ServiceLifetime.Transient:
                        _ = builder.Services.AddTransient(page);
                        if (vm is not null)
                        {
                            _ = builder.Services.AddTransient(vm);
                        }

                        break;
                    default:
                        break;
                }
            }

            // Services
            _ = builder.Services.AddSingleton<AuthService>();
            _ = builder.Services.AddSingleton<HealthService>();
            _ = builder.Services.AddSingleton<UserService>();

            // Master Level
            _ = builder.Services.AddSingleton<AppShell>();
            _ = builder.Services.AddSingleton<ShellViewModel>();

            _ = builder.Services.AddSingleton<FlyoutHeader>();

            //_ = builder.Services.AddTransient<IntroPage>();

            //_ = builder.Services.AddSingleton<VappsMasterPage>();
            //_ = builder.Services.AddSingleton<VappsMasterPageViewModel>();

            //_ = builder.Services.AddSingleton<SettingsPage>();
            //_ = builder.Services.AddSingleton<SettingsPageViewModel>();

            //_ = builder.Services.AddTransient<DefaultModalPage>();
            //_ = builder.Services.AddTransient<DefaultModalPageViewModel>();

            // Auth
            //_ = builder.Services.AddSingleton<LoginPage>();
            //_ = builder.Services.AddSingleton<LoginPageViewModel>();

            //_ = builder.Services.AddSingleton<RegisterPage>();
            //_ = builder.Services.AddSingleton<RegisterPageViewModel>();

            // Gastro App
            //_ = builder.Services.AddSingleton<GastroAppMasterPageViewModel>();
            //_ = builder.Services.AddSingleton<GastroAppMasterPage>();

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