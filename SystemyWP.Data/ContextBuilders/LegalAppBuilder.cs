using SystemyWP.Data.Models.LegalAppModels.Access;
using SystemyWP.Data.Models.LegalAppModels.Clients;
using SystemyWP.Data.Models.LegalAppModels.Clients.Cases;
using SystemyWP.Data.Models.LegalAppModels.Contacts;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.Data.ContextBuilders
{
    public static class LegalAppBuilder
    {
        public static void ConfigureLegalApp(this ModelBuilder modelBuilder)
        {
            #region Legal App Contacts Specific

            modelBuilder.Entity<LegalAppEmailAddress>()
                .HasOne<LegalAppContactDetails>()
                .WithMany(x => x.Emails)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LegalAppPhoneNumber>()
                .HasOne<LegalAppContactDetails>()
                .WithMany(x => x.PhoneNumbers)
                .OnDelete(DeleteBehavior.Cascade);  
            
            modelBuilder.Entity<LegalAppPhysicalAddress>()
                .HasOne<LegalAppContactDetails>()
                .WithMany(x => x.PhysicalAddresses)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
            
            #region Legal App Specific
            
            //Access Key setup
            modelBuilder.Entity<LegalAppAccessKey>()
                .HasMany(c => c.Users)
                .WithOne(e => e.LegalAppAccessKey)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<LegalAppAccessKey>()
                .HasMany(x => x.LegalAppClients)
                .WithOne(x => x.LegalAppAccessKey)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<LegalAppAccessKey>()
                .HasMany(x => x.LegalAppReminders)
                .WithOne(x => x.LegalAppAccessKey)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<LegalAppAccessKey>()
                .HasMany(x => x.LegalAppClientBillingData)
                .WithOne(x => x.LegalAppAccessKey)
                .OnDelete(DeleteBehavior.Cascade);
            
            //Contacts setup
            modelBuilder.Entity<LegalAppContactDetails>()
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
        }
    }
}