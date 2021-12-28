using Microsoft.EntityFrameworkCore;
using SystemyWP.API.Data.ContextBuilders;
using SystemyWP.API.Data.Models.General.Logging;
using SystemyWP.API.Data.Models.RestaurantAppModels.Dishes;
using SystemyWP.API.Data.Models.RestaurantAppModels.Ingredients;
using SystemyWP.API.Data.Models.RestaurantAppModels.Menus;
using SystemyWP.API.Data.Models.UsersManagement;
using SystemyWP.API.Data.Models.UsersManagement.Access;

namespace SystemyWP.API.Data
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
        
        public DbSet<AccessKey> AccessKeys { get; set; }
        
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