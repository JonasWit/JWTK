using Microsoft.EntityFrameworkCore;
using SystemyWP.API.Data.Models.UsersManagement;
using SystemyWP.API.Data.Models.UsersManagement.Access;

namespace SystemyWP.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AccessKey> AccessKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(x => x.AccessKey)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Claims)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AccessKey>()
                .HasMany(x => x.AllowedUsers)
                .WithMany(x => x.AllowedKeys);
        }
    }
}