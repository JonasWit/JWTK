using SystemyWP.API.Test.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.WebHost.ConfigureKestrel(options => options.ListenLocalhost(5013));

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile("secrets/appsettings.secrets.test.json", optional: true, reloadOnChange: true);
});

builder.Services.AddControllers();
builder.Services.Configure<SecretSettings>(configuration.GetSection(nameof(SecretSettings)));

var app = builder.Build();

app.MapControllers();

app.Run();