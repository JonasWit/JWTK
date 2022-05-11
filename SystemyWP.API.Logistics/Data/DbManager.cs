using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Logistics.Data;

public class DbManager
{
    public static void PrepareDatabase(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        UpdateDatabase(serviceScope.ServiceProvider.GetRequiredService<AppDbContext>());
    }

    private static void UpdateDatabase(DbContext context)
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