using Microsoft.EntityFrameworkCore;

namespace SystemyWP.API.Gastronomy.Data;

public class DBManager
{
    public static void PrepareDatabase(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        SeedData(serviceScope.ServiceProvider.GetRequiredService<AppDbContext>());
    }

    private static void SeedData(DbContext context)
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