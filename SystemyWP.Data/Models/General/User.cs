using System;
using System.Collections.Generic;
using Systemywp.Data.DataAccessModifiers;
using Systemywp.Data.Models.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Systemywp.Data.Models.General
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
        public string Nip { get; set; }
        [ProtectedPersonalData]
        public string Regon { get; set; }
        [ProtectedPersonalData]
        public string Krs { get; set; }
        
        public DateTime? LastLogin  { get; set; }  

        public List<DataAccess> DataAccess { get; set; } =
            new List<DataAccess>();
    }
}