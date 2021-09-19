using Microsoft.EntityFrameworkCore;
using SystemyWP.Data.Models.MedicalAppModels.Access;

namespace SystemyWP.Data.ContextBuilders
{
    public static class MedicalAppBuilder
    {
        public static void ConfigureMedicalApp(this ModelBuilder modelBuilder)
        {
            #region Medical App Access Key

            modelBuilder.Entity<MedicalAccessKey>()
                .HasMany(c => c.MedicalAppDataAccesses)
                .WithOne(e => e.MedicalAccessKey)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<MedicalAccessKey>()
                .HasMany(c => c.Users)
                .WithOne(e => e.MedicalAccessKey)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<MedicalAccessKey>()
                .HasMany(x => x.MedicalAppPatients)
                .WithOne(x => x.MedicalAccessKey)
                .OnDelete(DeleteBehavior.Cascade);  

            #endregion
        }
    }
}