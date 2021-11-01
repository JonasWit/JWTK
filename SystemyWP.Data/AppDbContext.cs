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
using SystemyWP.Data.Models.LegalAppModels.Billings;
using SystemyWP.Data.Models.RestaurantAppModels.Access;
using SystemyWP.Data.Models.RestaurantAppModels.Access.DataAccessModifiers;
using SystemyWP.Data.Models.RestaurantAppModels.Dishes;
using SystemyWP.Data.Models.RestaurantAppModels.Ingredients;
using SystemyWP.Data.Models.RestaurantAppModels.Menus;

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
        
        public DbSet<PortalPublication> PortalPublications { get; set; }

        #endregion

        #region LegalApp
        
        public DbSet<LegalAppDataAccess> LegalAppDataAccesses { get; set; }
        public DbSet<LegalAccessKey> LegalAccessKeys { get; set; }

        //General
        public DbSet<LegalAppReminder> LegalAppReminders { get; set; }

        //Client Tree
        public DbSet<LegalAppBillingRecord> LegalAppBillingRecords { get; set; }
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
        
        public DbSet<MedicalAppDataAccess> MedicalAppDataAccesses { get; set; }
        public DbSet<MedicalAccessKey> MedicalAccessKeys { get; set; }

        //Data
        public DbSet<MedicalAppPatient> MedicalAppPatients { get; set; }

        #endregion

        #region Restaurant App
        
        public DbSet<RestaurantAppDataAccess> RestaurantAppDataAccesses { get; set; }
        public DbSet<RestaurantAccessKey> RestaurantAccessKeys { get; set; }
        
        //Data
        public DbSet<RestaurantAppMenu> RestaurantAppMenus { get; set; }
        public DbSet<RestaurantAppDish> RestaurantAppDishes { get; set; }      
        public DbSet<RestaurantAppIngredient> RestaurantAppIngredients { get; set; }  
        
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Access Specific

            modelBuilder.Entity<LegalAppDataAccess>()
                .HasIndex(x => x.ItemId);
            modelBuilder.Entity<MedicalAppDataAccess>()
                .HasIndex(x => x.ItemId);
            modelBuilder.Entity<RestaurantAppDataAccess>()
                .HasIndex(x => x.ItemId);
            
            //General
            modelBuilder.Entity<User>()
                .HasMany(x => x.LegalAppDataAccesses)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<User>()
                .HasMany(x => x.MedicalAppDataAccesses)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<User>()
                .HasMany(x => x.RestaurantAppDataAccesses)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            modelBuilder.ConfigureLegalApp();
            modelBuilder.ConfigureMedicalApp();
            modelBuilder.ConfigureRestaurantApp();
        }
    }
}