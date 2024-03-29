using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using SystemyWP.API.Gastronomy.Data;
using SystemyWP.API.Gastronomy.Data.Repositories;
using SystemyWP.API.Gastronomy.Data.Repositories.RepositoriesInterfaces;
using SystemyWP.API.Gastronomy.Middleware;

var builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment()) builder.WebHost.ConfigureKestrel(options => options.ListenLocalhost(5001));

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

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Gastronomy"), serverAction =>
{
    _ = serverAction.EnableRetryOnFailure(3);
    _ = serverAction.CommandTimeout(20);
}));

builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IDishRepository, DishRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();

var app = builder.Build();

app.UseServiceStatus();
app.MapControllers();

DbManager.PrepareDatabase(app);

Console.WriteLine("--> App has started...");
app.Run();
