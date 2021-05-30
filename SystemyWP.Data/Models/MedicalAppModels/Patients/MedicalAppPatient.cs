using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.MedicalAppModels.Access;

namespace SystemyWP.Data.Models.MedicalAppModels.Patients
{
    public class MedicalAppPatient : Person<long>
    {
        [Required]
        public MedicalAccessKey MedicalAccessKey { get; set; }
        [Required]
        public int MedicalAccessKeyId { get; set; }
    }
}