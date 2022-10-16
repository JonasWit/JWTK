using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using VappsWeb;
using VappsWeb.Config;
using VappsWeb.Services;
using VappsWeb.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(new HttpClientPolicy());

if (builder.HostEnvironment.IsProduction())
{
    _ = builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(AppConfig.ApiRoutes.MasterService) });
}

if (builder.HostEnvironment.IsDevelopment())
{
    _ = builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(AppConfig.ApiRoutes.MasterServiceDEV) });
}

builder.Services.AddMudServices();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddBlazoredLocalStorage();
await builder.Build().RunAsync();
