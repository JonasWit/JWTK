using Microsoft.EntityFrameworkCore;
using MasterService.API.Gastronomy.Data.Models;

namespace MasterService.API.Gastronomy.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dish>()
                .HasMany(x => x.Ingredients)
                .WithMany(x => x.Dishes);

            modelBuilder.Entity<Menu>()
                .HasMany(x => x.Dishes)
                .WithMany(x => x.Menus);
        }
    }
}