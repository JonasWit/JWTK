using SystemyWP.Data.ContextBuilders;
using SystemyWP.Data.Models.General;
using SystemyWP.Data.Models.LegalAppModels.Access;
using SystemyWP.Data.Models.LegalAppModels.Access.DataAccessModifiers;
using SystemyWP.Data.Models.LegalAppModels.Clients;
using SystemyWP.Data.Models.LegalAppModels.Clients.Cases;
using SystemyWP.Data.Models.LegalAppModels.Contacts;
using SystemyWP.Data.Models.LegalAppModels.Reminders;
using SystemyWP.Data.Models.MedicalAppModels.Access;
using SystemyWP.Data.Models.MedicalAppModels.Access.DataAccessModifiers;
using SystemyWP.Data.Models.MedicalAppModels.Patients;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        #region Portal

        public DbSet<User> Users { get; set; }
        public DbSet<PortalLogRecord> PortalLogs { get; set; }
        public DbSet<ApiLogRecord> ApiLogs { get; set; }

        #endregion

        #region Applications

        //Access
        public DbSet<LegalAppDataAccess> LegalAppDataAccesses { get; set; }
        public DbSet<MedicalAppDataAccess> MedicalAppDataAccesses { get; set; }

        #endregion

        #region LegalApp

        //Access
        public DbSet<LegalAppAccessKey> LegalAppAccessKeys { get; set; }

        //General
        public DbSet<LegalAppReminder> LegalAppReminders { get; set; }

        //Client Tree
        public DbSet<LegalAppBillingRecord> LegalAppBillingData { get; set; }
        public DbSet<LegalAppClient> LegalAppClients { get; set; }
        public DbSet<LegalAppClientWorkRecord> LegalAppClientWorkRecords { get; set; }
        public DbSet<LegalAppClientNote> LegalAppClientNotes { get; set; }

        //Contacts
        public DbSet<LegalAppContactDetail> LegalAppContacts { get; set; }
        public DbSet<LegalAppEmailAddress> LegalAppEmailAddresses { get; set; }
        public DbSet<LegalAppPhoneNumber> LegalAppPhoneNumbers { get; set; }
        public DbSet<LegalAppPhysicalAddress> LegalAppPhysicalAddresses { get; set; }

        //Case Tree
        public DbSet<LegalAppCase> LegalAppCases { get; set; }
        public DbSet<LegalAppCaseNote> LegalAppCaseNotes { get; set; }
        public DbSet<LegalAppCaseDeadline> LegalAppCaseDeadlines { get; set; }

        #endregion

        #region Medical App

        //Access
        public DbSet<MedicalAccessKey> MedicalAccessKeys { get; set; }

        public DbSet<MedicalAppPatient> MedicalAppPatients { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Access Specific

            modelBuilder.Entity<User>()
                .HasMany(x => x.LegalAppDataAccesses)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<User>()
                .HasMany(x => x.MedicalAppDataAccesses)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);

            //Legal
            
            modelBuilder.Entity<LegalAppAccessKey>()
                .HasMany(c => c.LegalAppDataAccesses)
                .WithOne(e => e.LegalAppAccessKey)
                .OnDelete(DeleteBehavior.Cascade);
            
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

            //Medical
            
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

            modelBuilder.ConfigureLegalApp();
            modelBuilder.ConfigureMedicalApp();
        }
    }
}