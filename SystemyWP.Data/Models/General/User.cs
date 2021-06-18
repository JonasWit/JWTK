using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.LegalAppModels.Access;
using SystemyWP.Data.Models.MedicalAppModels.Access;
using Microsoft.AspNetCore.Identity;

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
        
        public LegalAppAccessKey LegalAppAccessKey { get; set; }
        public MedicalAccessKey MedicalAccessKey { get; set; }

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

        public List<DataAccess> DataAccess { get; set; } =
            new ();
    }
}