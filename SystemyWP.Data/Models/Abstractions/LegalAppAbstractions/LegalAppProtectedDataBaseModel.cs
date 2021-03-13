namespace SystemyWP.Data.Models.Abstractions.LegalAppAbstractions
{
    public class LegalAppBaseModel<Tkey> : BaseModel<Tkey>
    {
        public string DataAccessKey { get; set; }
    }
}