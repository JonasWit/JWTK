using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using SystemyWP.API.Gastronomy.Data;
using SystemyWP.API.Gastronomy.Middleware;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("Default")));

builder.Services.AddControllers()
    .AddFluentValidation(x =>
        x.RegisterValidatorsFromAssembly(typeof(Program).Assembly));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();

var app = builder.Build();

app.UseServiceStatus();
app.MapControllers();
DBManager.PrepareDatabase(app);
app.Run();