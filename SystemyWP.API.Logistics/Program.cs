using Microsoft.EntityFrameworkCore;
using Serilog;
using SystemyWP.API.Logistics.Data;
using SystemyWP.API.Logistics.Middleware;

var builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment()) builder.WebHost.ConfigureKestrel(options => options.ListenLocalhost(5002));

builder.WebHost.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile("secrets/appsettings.secrets.json", optional: true, reloadOnChange: true);
});

if (builder.Environment.IsDevelopment())
{
    builder.Host.UseSerilog((context, config) =>
    {
        config.WriteTo.Console();
    }); 
}

var configuration = builder.Configuration;

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Gastronomy")));
// builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
// builder.Services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();

var app = builder.Build();

app.UseServiceStatus();
app.MapControllers();

DbManager.PrepareDatabase(app);

Console.WriteLine("--> App has started...");
app.Run();