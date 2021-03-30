using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.General;
using SystemyWP.Data.Models.LegalAppModels.Cases;
using SystemyWP.Data.Models.LegalAppModels.Clients;
using SystemyWP.Data.Models.LegalAppModels.Reminders;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        #region Portal

        public DbSet<User> Users { get; set; }
        public DbSet<AccessKey> AccessKeys { get; set; }
        public DbSet<PortalLog> PortalLogs { get; set; }      
        
        #endregion

        #region Applications

        //Access
        public DbSet<DataAccess> DataAccesses { get; set; }  
        
        #endregion
        
        #region LegalApp
        
        //General
        public DbSet<LegalAppReminder> LegalAppReminders { get; set; }        
        
        //Client Tree
        public DbSet<LegalAppClient> LegalAppClients { get; set; }  
        public DbSet<LegalAppClientContactPerson> LegalAppClientContactPersons { get; set; }   
        public DbSet<LegalAppClientFinance> LegalAppClientFinances { get; set; }   
        public DbSet<LegalAppClientNote> LegalAppClientNotes { get; set; }
        
        //Case Tree
        public DbSet<LegalAppCase> LegalAppCases { get; set; }          
        public DbSet<LegalAppCaseNote> LegalAppCaseNotes { get; set; } 
        public DbSet<LegalAppClientContactPerson> LegalAppClientContactPeople { get; set; }
        public DbSet<LegalAppCaseDeadline> LegalAppCaseDeadlines { get; set; }    
        
        #endregion
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Legal App Access Specific

            modelBuilder.Entity<User>()
                .HasMany(x => x.DataAccess)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AccessKey>()
                .HasMany(c => c.Users)
                .WithOne(e => e.AccessKey)
                .OnDelete(DeleteBehavior.NoAction);     
            
            #endregion
            
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
                .HasMany(c => c.LegalAppClientFinances)
                .WithOne(e => e.LegalAppClient)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<LegalAppClient>()
                .HasMany(c => c.LegalAppClientContactPersons)
                .WithOne(e => e.LegalAppClient)
                .OnDelete(DeleteBehavior.Cascade);     
            
            //Case relations
            modelBuilder.Entity<LegalAppCase>()
                .HasMany(c => c.LegalAppCaseContactPersons)
                .WithOne(e => e.LegalAppCase)
                .OnDelete(DeleteBehavior.Cascade);  
            
            modelBuilder.Entity<LegalAppCase>()
                .HasMany(c => c.LegalAppCaseNotes)
                .WithOne(e => e.LegalAppCase)
                .OnDelete(DeleteBehavior.Cascade); 
            
            modelBuilder.Entity<LegalAppCase>()
                .HasMany(c => c.LegalAppCaseDeadlines)
                .WithOne(e => e.LegalAppCase)
                .OnDelete(DeleteBehavior.Cascade); 
            
            //Client Finance Relations

            modelBuilder.Entity<LegalAppClientFinance>()
                .HasOne(x => x.User);
        }
    }
}