using Microsoft.EntityFrameworkCore;
using SystemyWP.API.Gastronomy.Data.Models.Dishes;
using SystemyWP.API.Gastronomy.Data.Models.Ingredients;
using SystemyWP.API.Gastronomy.Data.Models.Menus;

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
        public DbSet<RestaurantAppUsedIngredient> RestaurantAppUsedIngredients { get; set; }  
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RestaurantAppDish>()
                .HasMany(x => x.RestaurantAppUsedIngredients)
                .WithOne(x => x.RestaurantAppDish)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<RestaurantAppMenu>()
                .HasMany(x => x.RestaurantAppDishes)
                .WithMany(x => x.RestaurantAppMenus);
        }
    }
}