using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SystemyWP.API.Gastronomy.Data;
using SystemyWP.API.Gastronomy.Middleware;
using SystemyWP.API.Gastronomy.Repositories;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.WebHost.ConfigureKestrel(options => options.ListenLocalhost(5001));
}

var configuration = builder.Configuration;

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Gastronomy")));
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();

var app = builder.Build();

app.UseServiceStatus();
app.MapControllers();

//DBManager.PrepareDatabase(app);

app.Run();