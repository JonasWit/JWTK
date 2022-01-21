using System;
using SystemyWP.API.Services.Email;
using SystemyWP.API.Settings;
using AspNetCoreRateLimit;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SystemyWP.API.Middleware;
using SystemyWP.API.Repositories.General;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using SystemyWP.API;
using SystemyWP.API.Data;
using SystemyWP.API.Services.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options => options.ListenLocalhost(5000));
builder.WebHost.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile("secrets/appsettings.secrets.json", optional: true, reloadOnChange: true);
});

builder.WebHost.UseSerilog((context, config) =>
{
    var connectionString = context.Configuration.GetConnectionString("Master");
    config.WriteTo.PostgreSQL(connectionString, "Logs", null, LogEventLevel.Verbose, needAutoCreateTable: false, needAutoCreateSchema: false)
        .MinimumLevel.Information();
});

var configuration = builder.Configuration;

builder.Services.AddOptions();
builder.Services.AddMemoryCache();

//IP Rate limiting
builder.Services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
builder.Services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));
builder.Services.AddInMemoryRateLimiting();

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Master")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
            
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwtBearerOptions =>
{
    jwtBearerOptions.RequireHttpsMetadata = true;
    jwtBearerOptions.SaveToken = true;
    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue("AuthSettings:SecretKey", ""))),
        ClockSkew = TimeSpan.Zero,
        ValidateIssuer = true,
        ValidateAudience = true,
    };
});
            
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(SystemyWpConstants.Policies.User, policy => policy
        .RequireAuthenticatedUser()
        .RequireClaim(SystemyWpConstants.Claims.Role,
            SystemyWpConstants.Roles.User,
            SystemyWpConstants.Roles.Admin));

    options.AddPolicy(SystemyWpConstants.Policies.Admin, policy => policy
        .RequireAuthenticatedUser()
        .RequireClaim(SystemyWpConstants.Claims.Role,
            SystemyWpConstants.Roles.Admin));
});

builder.Services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssembly(typeof(Program).Assembly));

// Options from Settings
builder.Services.Configure<ClusterServices>(configuration.GetSection(nameof(ClusterServices)));
builder.Services.Configure<SendGridOptions>(configuration.GetSection(nameof(SendGridOptions)));
builder.Services.Configure<CorsSettings>(configuration.GetSection(nameof(CorsSettings)));
builder.Services.Configure<AuthSettings>(configuration.GetSection(nameof(AuthSettings)));
            
builder.Services.AddScoped<EmailClient>();
builder.Services.AddTransient<Encryptor>();

builder.Services.AddFileServices(configuration);

builder.Services.AddCors(options => options.AddPolicy(SystemyWpConstants.CorsName.ClientApp, build => build
    .AllowAnyHeader()
    .WithOrigins(configuration.GetValue("CorsSettings:PortalUrl", ""))
    .AllowAnyMethod()
    .AllowCredentials()));

builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCustomResponseHeaders();
app.UseCors(SystemyWpConstants.CorsName.ClientApp);
app.UseStaticFiles();
app.UseRouting();
app.UseIpRateLimiting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});
            
DBManager.PrepareDatabase(app);
Console.WriteLine("--> App has started...");

app.Run();