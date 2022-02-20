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
        public DbSet<RestaurantAppMenu> RestaurantAppMenus { get; set; }
        public DbSet<RestaurantAppDish> RestaurantAppDishes { get; set; }      
        public DbSet<RestaurantAppIngredient> RestaurantAppIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RestaurantAppDish>()
                .HasMany(x => x.RestaurantAppIngredients)
                .WithMany(x => x.RestaurantAppDishes);

            modelBuilder.Entity<RestaurantAppMenu>()
                .HasMany(x => x.RestaurantAppDishes)
                .WithMany(x => x.RestaurantAppMenus);
        }
    }
}