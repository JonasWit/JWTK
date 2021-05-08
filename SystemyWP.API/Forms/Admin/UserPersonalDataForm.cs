namespace Systemywp.Api.Forms.Admin
{
    public class UserPersonalDataForm : UserIdForm
    {
        public string PhoneNumber { get; set; }
        public string CompanyFullName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string AddressCorrespondence { get; set; }
        public string City { get; set; }
        public string Vivodership { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        public string Nip { get; set; }
        public string Regon { get; set; }
        public string Krs { get; set; }
    }
}