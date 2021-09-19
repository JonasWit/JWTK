using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.LegalAppModels.Access;
using SystemyWP.Data.Models.LegalAppModels.Access.DataAccessModifiers;
using SystemyWP.Data.Models.MedicalAppModels.Access;
using SystemyWP.Data.Models.MedicalAppModels.Access.DataAccessModifiers;
using Microsoft.AspNetCore.Identity;
using SystemyWP.Data.Models.RestaurantAppModels.Access;
using SystemyWP.Data.Models.RestaurantAppModels.Access.DataAccessModifiers;

namespace SystemyWP.Data.Models.General
{
    public class User : BaseModel<string>
    {
        public string Image { get; set; }

        [Required]
        [MaxLength(265)]
        public string Email { get; set; }
        
        [Required]
        [MaxLength(265)]
        public string Username { get; set; }

        [ProtectedPersonalData]
        public string PhoneNumber { get; set; }
        [ProtectedPersonalData]
        public string CompanyFullName { get; set; }
        [ProtectedPersonalData]
        public string Name { get; set; }
        [ProtectedPersonalData]
        public string Surname { get; set; }
        [ProtectedPersonalData]
        public string Address { get; set; }
        [ProtectedPersonalData]
        public string AddressCorrespondence { get; set; }
        [ProtectedPersonalData]
        public string City { get; set; }
        [ProtectedPersonalData]
        public string Vivodership { get; set; }
        [ProtectedPersonalData]
        public string Country { get; set; }
        [ProtectedPersonalData]
        public string PostCode { get; set; }
        [ProtectedPersonalData]
        public string Nip { get; set; }
        [ProtectedPersonalData]
        public string Regon { get; set; }
        [ProtectedPersonalData]
        public string Krs { get; set; }
        
        public DateTime? LastLogin  { get; set; }  
        
        public LegalAccessKey LegalAccessKey { get; set; }
        public MedicalAccessKey MedicalAccessKey { get; set; }
        public RestaurantAccessKey RestaurantAccessKey { get; set; }

        public List<LegalAppDataAccess> LegalAppDataAccesses { get; set; } = new ();
        public List<MedicalAppDataAccess> MedicalAppDataAccesses { get; set; } = new ();
        public List<RestaurantAppDataAccess> RestaurantAppDataAccesses { get; set; } = new ();
    }
}