using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.General;
using SystemyWP.Data.Models.LegalAppModels.Access;
using SystemyWP.Data.Models.LegalAppModels.Cases;
using SystemyWP.Data.Models.LegalAppModels.Clients;
using SystemyWP.Data.Models.LegalAppModels.Contact;
using SystemyWP.Data.Models.LegalAppModels.Reminders;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        #region Portal

        public DbSet<User> Users { get; set; }
        public DbSet<LegalAppAccessKey> AccessKeys { get; set; }
        public DbSet<PortalLogRecord> PortalLogs { get; set; }
        public DbSet<ApiLogRecord> ApiLogs { get; set; }     
        
        #endregion

        #region Applications

        //Access
        public DbSet<DataAccess> DataAccesses { get; set; }
        public DbSet<LegalAppContactDetails> Contacts { get; set; }
        public DbSet<LegalAppEmailAddress> EmailAddresses { get; set; }       
        public DbSet<LegalAppPhoneNumber> PhoneNumbers { get; set; }
        public DbSet<LegalAppPhysicalAddress> PhysicalAddresses { get; set; }
        
        #endregion
        
        #region LegalApp
        
        //General
        public DbSet<LegalAppReminder> LegalAppReminders { get; set; }       
        
        //Client Tree
        public DbSet<LegalAppClient> LegalAppClients { get; set; }
        public DbSet<LegalAppClientWorkRecord> LegalAppClientWorkRecords { get; set; }
        public DbSet<LegalAppClientNote> LegalAppClientNotes { get; set; }
        
        //Case Tree
        public DbSet<LegalAppCase> LegalAppCases { get; set; }
        public DbSet<LegalAppCaseNote> LegalAppCaseNotes { get; set; }
        public DbSet<LegalAppCaseDeadline> LegalAppCaseDeadlines { get; set; }
        
        #endregion
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
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

            #region Access Specific

            modelBuilder.Entity<User>()
                .HasMany(x => x.DataAccess)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LegalAppAccessKey>()
                .HasMany(c => c.Users)
                .WithOne(e => e.LegalAppAccessKey)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<LegalAppAccessKey>()
                .HasMany(x => x.LegalAppClients)
                .WithOne(x => x.LegalAppAccessKey)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Legal App Specific

            modelBuilder.Entity<LegalAppContactDetails>()
                .HasOne<LegalAppClient>()
                .WithMany(x => x.Contacts)
                .OnDelete(DeleteBehavior.Cascade);

            //Client relations
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

            //Case relations
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