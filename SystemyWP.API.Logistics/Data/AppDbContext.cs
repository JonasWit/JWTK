using Microsoft.EntityFrameworkCore;
using SystemyWP.API.Logistics.Data.Models;

namespace SystemyWP.API.Logistics.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }       

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}