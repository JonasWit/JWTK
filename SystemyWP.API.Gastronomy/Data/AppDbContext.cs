using Microsoft.EntityFrameworkCore;
using SystemyWP.API.Gastronomy.Data.Models;

namespace SystemyWP.API.Gastronomy.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        //Data
        public DbSet<Menu> RestaurantAppMenus { get; set; }
        public DbSet<Dish> RestaurantAppDishes { get; set; }      
        public DbSet<Ingredient> RestaurantAppIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dish>()
                .HasMany(x => x.RestaurantAppIngredients)
                .WithMany(x => x.RestaurantAppDishes);

            modelBuilder.Entity<Menu>()
                .HasMany(x => x.RestaurantAppDishes)
                .WithMany(x => x.RestaurantAppMenus);
        }
    }
}