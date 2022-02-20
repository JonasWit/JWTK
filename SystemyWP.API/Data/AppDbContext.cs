using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SystemyWP.API.Data.Models;

namespace SystemyWP.API.Data
{
    public class AppDbContext : DbContext, IDataProtectionKeyContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
        public DbSet<Log> Logs { get; set; }    
        public DbSet<User> Users { get; set; }    
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<AccessKey> AccessKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>().HasNoKey();
            
            modelBuilder.Entity<User>()
                .HasOne(x => x.AccessKey)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Claims)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Log>()
                .Property(b => b.Properties)
                .HasColumnType("jsonb");
        }
    }
}