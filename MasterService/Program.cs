using AspNetCoreRateLimit;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MasterService.API.Constants;
using MasterService.API.Data;
using MasterService.API.Data.Repositories;
using MasterService.API.Middleware;
using MasterService.API.Policies;
using MasterService.API.Profiles;
using MasterService.API.Services.Auth;
using MasterService.API.Services.Email;
using MasterService.API.Services.HttpServices;
using MasterService.API.Services.JWTServices;
using MasterService.API.Settings;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PostgreSQL.ColumnWriters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Text;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment())
{
    _ = builder.WebHost.ConfigureKestrel(options => options.ListenLocalhost(5000));
}

// Secrets injection
builder.WebHost.ConfigureAppConfiguration((hostingContext, config) =>
{
    _ = config.AddJsonFile(AppConstants.Paths.SecretSettings, true, true);
});

// Logging
if (builder.Environment.IsProduction())
{
    _ = builder.Host.UseSerilog((context, config) =>
    {
        var connectionString = context.Configuration.GetConnectionString("Master");

        IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
        {
            {"Message", new RenderedMessageColumnWriter(NpgsqlDbType.Text)},
            {"MessageTemplate", new MessageTemplateColumnWriter(NpgsqlDbType.Text)},
            {"Level", new LevelColumnWriter(true, NpgsqlDbType.Varchar)},
            {"RaiseDate", new TimestampColumnWriter(NpgsqlDbType.Timestamp)},
            {"Exception", new ExceptionColumnWriter(NpgsqlDbType.Text)},
            {"Properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb)}
        };

        _ = config.WriteTo.PostgreSQL(connectionString, "Logs", columnWriters)
            .MinimumLevel.Information();
    });
}

if (builder.Environment.IsDevelopment())
{
    _ = builder.Host.UseSerilog((context, config) => { _ = config.WriteTo.Console(); });
}

// Swagger
if (builder.Environment.IsDevelopment())
{
    _ = builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "systemywp", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
    });
    _ = builder.Services.AddFluentValidationRulesToSwagger();
}

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddOptions();
builder.Services.AddMemoryCache();

// IP Rate limiting
builder.Services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
builder.Services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));
builder.Services.AddInMemoryRateLimiting();

// Data Access Layer
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Master"), serverAction =>
{
    _ = serverAction.EnableRetryOnFailure(3);
    _ = serverAction.CommandTimeout(20);
}));

builder.Services.AddScoped<UserRepository>();

// Mapper with additional data from URL Service
builder.Services.AddSingleton(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new GastronomyServiceProfile(provider.GetService<UrlService>()));
}).CreateMapper());

// Polly policies
builder.Services.AddSingleton(new HttpClientPolicy());

builder.Services.AddDataProtection()
    .SetApplicationName("systemywp")
    .UseCryptographicAlgorithms(
        new AuthenticatedEncryptorConfiguration
        {
            EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
            ValidationAlgorithm = ValidationAlgorithm.HMACSHA512
        })
    .PersistKeysToDbContext<AppDbContext>();

builder.Services.AddAuthentication("OAuth").AddJwtBearer("OAuth", config =>
{
    var secretBytes = Encoding.UTF8.GetBytes(configuration.GetValue("AuthSettings:SecretKey", ""));
    var key = new SymmetricSecurityKey(secretBytes);

    if (builder.Environment.IsDevelopment())
    {
        config.RequireHttpsMetadata = false;
    }

    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = configuration.GetValue("AuthSettings:Issuer", ""),
        ValidAudience = configuration.GetValue("AuthSettings:Audience", ""),
        IssuerSigningKey = key,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidateIssuer = true,
        ValidateAudience = true
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(AppConstants.Policies.User, policy => policy
        .RequireAuthenticatedUser()
        .RequireClaim(ClaimTypes.Role,
            AppConstants.Roles.User,
            AppConstants.Roles.Admin));

    options.AddPolicy(AppConstants.Policies.Admin, policy => policy
        .RequireAuthenticatedUser()
        .RequireClaim(ClaimTypes.Role,
            AppConstants.Roles.Admin));
});

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Options from Settings
builder.Services.Configure<ClusterServices>(configuration.GetSection(nameof(ClusterServices)));
builder.Services.Configure<EmailClientOptions>(configuration.GetSection(nameof(EmailClientOptions)));
builder.Services.Configure<CorsSettings>(configuration.GetSection(nameof(CorsSettings)));
builder.Services.Configure<AuthSettings>(configuration.GetSection(nameof(AuthSettings)));

builder.Services.AddHttpClient<GastronomyHttpClient>(httpClient =>
    httpClient.BaseAddress = new Uri(configuration.GetValue<string>("ClusterServices:GastronomyService")));

builder.Services.AddScoped<EmailClient>();

builder.Services.AddTransient<Encryptor>();
builder.Services.AddTransient<TokenService>();
builder.Services.AddSingleton<UrlService>();

builder.Services.AddFileServices(configuration);

builder.Services.AddCors(options => options.AddPolicy(AppConstants.CorsName.ClientApp, build => build
    .AllowAnyHeader()
    .WithOrigins(configuration.GetValue("CorsSettings:PortalUrl", ""))
    .AllowAnyMethod()
    .AllowCredentials()));

if (builder.Environment.IsProduction())
{
    _ = builder.Services.AddHsts(options =>
    {
        options.Preload = true;
        options.MaxAge = TimeSpan.FromDays(60);
    });

    _ = builder.Services.AddHttpsRedirection(options =>
    {
        options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
        options.HttpsPort = 443;
    });
}

builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

// Build the app
WebApplication app = builder.Build();

// Dev only
if (app.Environment.IsDevelopment())
{
    _ = app.UseDeveloperExceptionPage();
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

// Prod only
if (app.Environment.IsProduction())
{
    _ = app.UseExceptionHandler("/Error");
    _ = app.UseHsts();
}

app.UseIpRateLimiting();
app.UseCustomResponseHeaders();
app.UseRouting();
app.UseCors(AppConstants.CorsName.ClientApp);
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { _ = endpoints.MapDefaultControllerRoute(); });

DbManager.PrepareDatabase(app);
Console.WriteLine($"--> Master App Settings used: {app.Configuration.GetValue("ConfigSet", "No config Set")}");
Console.WriteLine("--> Master App has started...");

app.Run();