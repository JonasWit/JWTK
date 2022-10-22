using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MasterService.API.Data;

public static class DbManager
{
    public static void PrepareDatabase(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
    }

    private static void SeedData(AppDbContext context)
    {
        Console.WriteLine("--> Attempting to apply migrations...");
        try
        {
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Could not run migrations: {ex.Message}");
        }
    }
}