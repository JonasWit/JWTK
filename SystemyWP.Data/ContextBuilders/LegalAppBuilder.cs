using SystemyWP.Data.Models.LegalAppModels.Clients;
using SystemyWP.Data.Models.LegalAppModels.Clients.Cases;
using SystemyWP.Data.Models.LegalAppModels.Contacts;
using Microsoft.EntityFrameworkCore;
using SystemyWP.Data.Models.LegalAppModels.Access;

namespace SystemyWP.Data.ContextBuilders
{
    public static class LegalAppBuilder
    {
        public static void ConfigureLegalApp(this ModelBuilder modelBuilder)
        {
            #region Legal App Contacts Specific

            modelBuilder.Entity<LegalAppEmailAddress>()
                .HasOne<LegalAppContactDetail>()
                .WithMany(x => x.Emails)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LegalAppPhoneNumber>()
                .HasOne<LegalAppContactDetail>()
                .WithMany(x => x.PhoneNumbers)
                .OnDelete(DeleteBehavior.Cascade);  
            
            modelBuilder.Entity<LegalAppPhysicalAddress>()
                .HasOne<LegalAppContactDetail>()
                .WithMany(x => x.PhysicalAddresses)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
            
            #region Legal App Specific

            //Contacts setup
            modelBuilder.Entity<LegalAppContactDetail>()
                .HasOne<LegalAppClient>()
                .WithMany(x => x.Contacts)
                .OnDelete(DeleteBehavior.Cascade);

            //Client setup
            modelBuilder.Entity<LegalAppClient>()
                .HasMany(c => c.LegalAppCases)
                .WithOne(e => e.LegalAppClient)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<LegalAppClient>()
                .HasMany(c => c.LegalAppClientNotes)
                .WithOne(e => e.LegalAppClient)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LegalAppClient>()
                .HasMany(c => c.LegalAppClientWorkRecords)
                .WithOne(e => e.LegalAppClient)
                .OnDelete(DeleteBehavior.Cascade);

            //Case setup
            modelBuilder.Entity<LegalAppCase>()
                .HasMany(c => c.LegalAppCaseNotes)
                .WithOne(e => e.LegalAppCase)
                .OnDelete(DeleteBehavior.Cascade); 
            
            modelBuilder.Entity<LegalAppCase>()
                .HasMany(c => c.LegalAppCaseDeadlines)
                .WithOne(e => e.LegalAppCase)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Legal App Access Key
            
            modelBuilder.Entity<LegalAccessKey>()
                .HasMany(c => c.LegalAppDataAccesses)
                .WithOne(e => e.LegalAccessKey)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<LegalAccessKey>()
                .HasMany(c => c.Users)
                .WithOne(e => e.LegalAccessKey)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<LegalAccessKey>()
                .HasMany(x => x.LegalAppClients)
                .WithOne(x => x.LegalAccessKey)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LegalAccessKey>()
                .HasMany(x => x.LegalAppReminders)
                .WithOne(x => x.LegalAccessKey)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LegalAccessKey>()
                .HasMany(x => x.LegalAppClientBillingData)
                .WithOne(x => x.LegalAccessKey)
                .OnDelete(DeleteBehavior.Cascade);
            
            #endregion
        }
    }
}