using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models;
using SystemyWP.Data.Models.General;
using SystemyWP.Data.Models.LegalAppModels;
using Microsoft.EntityFrameworkCore;

namespace SystemyWP.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<User> Users { get; set; }
        public DbSet<AccessKey> AccessKeys { get; set; }
        public DbSet<PortalLog> PortalLogs { get; set; }      
        
        #region LegalApp

        public DbSet<DataAccess> DataAccesses { get; set; }  
        public DbSet<LegalAppClient> LegalAppClients { get; set; }       
        public DbSet<LegalAppCase> LegalAppCases { get; set; }          
        public DbSet<LegalAppCaseNote> LegalAppCaseNotes { get; set; } 
        public DbSet<LegalAppClientContactPerson> LegalAppClientContactPeople { get; set; }
        
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
            
            //Client`s inner objects
            modelBuilder.Entity<LegalAppClient>()
                .HasMany(c => c.Cases)
                .WithOne(e => e.LegalAppClient)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<LegalAppClient>()
                .HasMany(c => c.ContactPeople)
                .WithOne(e => e.LegalAppClient)
                .OnDelete(DeleteBehavior.Cascade);         
            
            
            //Case`s inner objects
            
            

        }
    }
}