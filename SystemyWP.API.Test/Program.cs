using SystemyWP.API.Test.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile("secrets/appsettings.secrets.test.json", optional: true, reloadOnChange: true);
});

builder.Services.AddControllers();
builder.Services.Configure<SecretSettings>(configuration.GetSection(nameof(SecretSettings)));

var app = builder.Build();

app.MapControllers();

Console.WriteLine("--> Test App has started...");

app.Run();