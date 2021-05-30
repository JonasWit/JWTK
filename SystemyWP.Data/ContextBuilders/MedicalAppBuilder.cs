using SystemyWP.Data.Models.MedicalAppModels.Access;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.Data.ContextBuilders
{
    public static class MedicalAppBuilder
    {
        public static void ConfigureMedicalApp(this ModelBuilder modelBuilder)
        {
            //Access setup
            modelBuilder.Entity<MedicalAccessKey>()
                .HasMany(c => c.Users)
                .WithOne(e => e.MedicalAccessKey)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<MedicalAccessKey>()
                .HasMany(x => x.MedicalAppPatients)
                .WithOne(x => x.MedicalAccessKey)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}