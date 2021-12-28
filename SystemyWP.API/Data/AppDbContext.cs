using SystemyWP.Data.ContextBuilders;
using SystemyWP.Data.Models.General;
using Microsoft.EntityFrameworkCore;
using SystemyWP.Data.Models.RestaurantAppModels.Access;
using SystemyWP.Data.Models.RestaurantAppModels.Dishes;
using SystemyWP.Data.Models.RestaurantAppModels.Ingredients;
using SystemyWP.Data.Models.RestaurantAppModels.Menus;

namespace SystemyWP.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //Portal
        public DbSet<User> Users { get; set; }
        public DbSet<PortalLogRecord> PortalLogs { get; set; }
        public DbSet<ApiLogRecord> ApiLogs { get; set; }
        
        //Restaurnat
        public DbSet<RestaurantAccessKey> RestaurantAccessKeys { get; set; }
        
        //Data
        public DbSet<RestaurantAppMenu> RestaurantAppMenus { get; set; }
        public DbSet<RestaurantAppDish> RestaurantAppDishes { get; set; }      
        public DbSet<RestaurantAppIngredient> RestaurantAppIngredients { get; set; }  
        public DbSet<RestaurantAppUsedIngredient> RestaurantAppUsedIngredients { get; set; }  
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureRestaurantApp();
        }
    }
}