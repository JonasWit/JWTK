using Microsoft.EntityFrameworkCore;
using SystemyWP.API.Data.Models.RestaurantAppModels.Dishes;
using SystemyWP.API.Data.Models.RestaurantAppModels.Menus;

namespace SystemyWP.API.Data.ContextBuilders
{
    public static class RestaurantAppBuilder
    {
        public static void ConfigureRestaurantApp(this ModelBuilder modelBuilder)
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