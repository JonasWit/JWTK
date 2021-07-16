using System.Collections.Generic;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.MedicalAppModels.Patients;

namespace SystemyWP.Data.Models.MedicalAppModels.Access
{
    public class MedicalAccessKey: AccessKeyBase<int>
    {
        public List<MedicalAppPatient> MedicalAppPatients { get; set; } = new();
    }
    
}