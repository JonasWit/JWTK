using Microsoft.EntityFrameworkCore;
using SystemyWP.Data.Models.RestaurantAppModels.Access;
using SystemyWP.Data.Models.RestaurantAppModels.Dishes;
using SystemyWP.Data.Models.RestaurantAppModels.Ingredients;

namespace SystemyWP.Data.ContextBuilders
{
    public static class RestaurantAppBuilder
    {
        public static void ConfigureRestaurantApp(this ModelBuilder modelBuilder)
        {
            #region Restaurant App Access Key

            modelBuilder.Entity<RestaurantAccessKey>()
                .HasMany(c => c.RestaurantAppDataAccesses)  
                .WithOne(e => e.RestaurantAccessKey)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RestaurantAccessKey>()
                .HasMany(c => c.Users)
                .WithOne(e => e.RestaurantAccessKey)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<RestaurantAccessKey>()
                .HasMany(x => x.RestaurantAppMenus)
                .WithOne(x => x.RestaurantAccessKey)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<RestaurantAccessKey>()
                .HasMany(x => x.RestaurantAppDishes)
                .WithOne(x => x.RestaurantAccessKey)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<RestaurantAccessKey>()
                .HasMany(x => x.RestaurantAppIngredients)
                .WithOne(x => x.RestaurantAccessKey)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            modelBuilder.Entity<RestaurantAppDish>()
                .HasMany(x => x.RestaurantAppUsedIngredients)
                .WithOne(x => x.RestaurantAppDish)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RestaurantAppIngredient>()
                .HasMany(x => x.RestaurantAppUsedIngredients)
                .WithOne(x => x.RestaurantAppIngredient)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}