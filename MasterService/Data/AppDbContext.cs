using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MasterService.API.Data.Models;

namespace MasterService.API.Data;

public class AppDbContext : DbContext, IDataProtectionKeyContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Log> Logs { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserClaim> UserClaims { get; set; }

    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<Log>().HasNoKey();

        _ = modelBuilder.Entity<User>()
            .HasMany(x => x.Claims)
            .WithOne(x => x.User)
            .OnDelete(DeleteBehavior.Cascade);

        _ = modelBuilder.Entity<Log>()
            .Property(b => b.Properties)
            .HasColumnType("jsonb");
    }
}