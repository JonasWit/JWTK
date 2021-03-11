﻿using SystemyWP.Data.Models.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace SystemyWP.Data.Models
{
    public class User : BaseModel<string>
    {
        public string Image { get; set; }
        public AccessKey AccessKey { get; set; }
        
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
        public string NIP { get; set; }
        [ProtectedPersonalData]
        public string REGON { get; set; }
        [ProtectedPersonalData]
        public string KRS { get; set; }
    }
}