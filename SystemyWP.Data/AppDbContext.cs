using SystemyWP.Data.ContextBuilders;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.General;
using SystemyWP.Data.Models.LegalAppModels.Access;
using SystemyWP.Data.Models.LegalAppModels.Cases;
using SystemyWP.Data.Models.LegalAppModels.Clients;
using SystemyWP.Data.Models.LegalAppModels.Contact;
using SystemyWP.Data.Models.LegalAppModels.Reminders;
using SystemyWP.Data.Models.MedicalAppModels.Access;
using SystemyWP.Data.Models.MedicalAppModels.Patients;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        #region Portal

        public DbSet<User> Users { get; set; }
        public DbSet<PortalLogRecord> PortalLogs { get; set; }
        public DbSet<ApiLogRecord> ApiLogs { get; set; }     
        
        #endregion

        #region Applications

        //Access
        public DbSet<DataAccess> DataAccesses { get; set; }
        
        #endregion
        
        #region LegalApp
        
        //Access
        public DbSet<LegalAppAccessKey> LegalAppAccessKeys { get; set; }
        
        //General
        public DbSet<LegalAppReminder> LegalAppReminders { get; set; }       
        
        //Client Tree
        public DbSet<LegalAppClient> LegalAppClients { get; set; }
        public DbSet<LegalAppClientWorkRecord> LegalAppClientWorkRecords { get; set; }
        public DbSet<LegalAppClientNote> LegalAppClientNotes { get; set; }
        
        //Contacts
        public DbSet<LegalAppContactDetails> LegalAppContacts { get; set; }
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
                .HasMany(x => x.DataAccess)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            modelBuilder.ConfigureLegalApp();
            modelBuilder.ConfigureMedicalApp();
            
        }
    }
}