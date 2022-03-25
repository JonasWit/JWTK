using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
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
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.IdentityModel.Tokens;
using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PostgreSQL.ColumnWriters;
using SystemyWP.API;
using SystemyWP.API.Data;
using SystemyWP.API.HttpClients;
using SystemyWP.API.Policies;
using SystemyWP.API.Repositories;
using SystemyWP.API.Services.Auth;
using SystemyWP.API.Services.JWTServices;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.WebHost.ConfigureKestrel(options => options.ListenLocalhost(5000));
}

builder.WebHost.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile("secrets/appsettings.secrets.json", optional: true, reloadOnChange: true);
});

if (builder.Environment.IsProduction())
{
    builder.Host.UseSerilog((context, config) =>
    {
        var connectionString = context.Configuration.GetConnectionString("Master");

        IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
        {
            {"Message", new RenderedMessageColumnWriter(NpgsqlDbType.Text)},
            {"MessageTemplate", new MessageTemplateColumnWriter(NpgsqlDbType.Text)},
            {"Level", new LevelColumnWriter(true, NpgsqlDbType.Varchar)},
            {"RaiseDate", new TimestampColumnWriter(NpgsqlDbType.Timestamp)},
            {"Exception", new ExceptionColumnWriter(NpgsqlDbType.Text)},
            {"Properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb)},
        };

        config.WriteTo.PostgreSQL(connectionString, "Logs", columnWriters)
            .MinimumLevel.Information();

        if (builder.Environment.IsDevelopment())
        {
            config.WriteTo.Console();
        }
    });  
}

if (builder.Environment.IsDevelopment())
{
    builder.Host.UseSerilog((context, config) =>
    {
        config.WriteTo.Console();
    }); 
}

var configuration = builder.Configuration;

builder.Services.AddOptions();
builder.Services.AddMemoryCache();

//IP Rate limiting
builder.Services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
builder.Services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));
builder.Services.AddInMemoryRateLimiting();

//Data Access Layer
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Master")));
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton(new HttpClientPolicy());

builder.Services.AddDataProtection()
    .SetApplicationName("systemywp")
    .UseCryptographicAlgorithms(
        new AuthenticatedEncryptorConfiguration()
        {
            EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
            ValidationAlgorithm = ValidationAlgorithm.HMACSHA512
        })
    .PersistKeysToDbContext<AppDbContext>();

builder.Services.AddAuthentication("OAuth").AddJwtBearer("OAuth", config =>
{
    var secretBytes = Encoding.UTF8.GetBytes(configuration.GetValue("AuthSettings:SecretKey", ""));
    var key = new SymmetricSecurityKey(secretBytes);
    
    if (builder.Environment.IsDevelopment()) config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = configuration.GetValue("AuthSettings:Issuer", ""),
        ValidAudience = configuration.GetValue("AuthSettings:Audience", ""),
        IssuerSigningKey = key,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidateIssuer = true,
        ValidateAudience = true,
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(SystemyWpConstants.Policies.User, policy => policy
        .RequireAuthenticatedUser()
        .RequireClaim(ClaimTypes.Role,
            SystemyWpConstants.Roles.User,
            SystemyWpConstants.Roles.Admin));

    options.AddPolicy(SystemyWpConstants.Policies.Admin, policy => policy
        .RequireAuthenticatedUser()
        .RequireClaim(ClaimTypes.Role,
            SystemyWpConstants.Roles.Admin));
});

builder.Services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssembly(typeof(Program).Assembly));

// Options from Settings
builder.Services.Configure<ClusterServices>(configuration.GetSection(nameof(ClusterServices)));
builder.Services.Configure<SendGridOptions>(configuration.GetSection(nameof(SendGridOptions)));
builder.Services.Configure<CorsSettings>(configuration.GetSection(nameof(CorsSettings)));
builder.Services.Configure<AuthSettings>(configuration.GetSection(nameof(AuthSettings)));

builder.Services.AddHttpClient<GastronomyHttpClient>();
builder.Services.AddScoped<EmailClient>();
builder.Services.AddTransient<Encryptor>();
builder.Services.AddTransient<TokenService>();

builder.Services.AddFileServices(configuration);

builder.Services.AddCors(options => options.AddPolicy(SystemyWpConstants.CorsName.ClientApp, build => build
    .AllowAnyHeader()
    .WithOrigins(configuration.GetValue("CorsSettings:PortalUrl", ""))
    .AllowAnyMethod()
    .AllowCredentials()));

if (builder.Environment.IsProduction())
{
    builder.Services.AddHsts(options =>
    {
        options.Preload = true;
        options.MaxAge = TimeSpan.FromDays(60);
    });

    builder.Services.AddHttpsRedirection(options =>
    {
        options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
        options.HttpsPort = 443;
    });  
}

builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

// Build the app
var app = builder.Build();

// Dev only
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Prod only
if (app.Environment.IsProduction())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseIpRateLimiting();
app.UseCustomResponseHeaders();
app.UseRouting();
app.UseCors(SystemyWpConstants.CorsName.ClientApp);
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });

DbManager.PrepareDatabase(app);
Console.WriteLine($"--> Settings used: {app.Configuration.GetValue("ConfigSet", "No config Set")}");
Console.WriteLine("--> App has started...");

app.Run();