namespace SystemyWP.Data.Models.Abstractions
{
    public class LegalAppBaseModel<Tkey> : BaseModel<Tkey>
    {
        public string DataAccessKey { get; set; }
    }
}