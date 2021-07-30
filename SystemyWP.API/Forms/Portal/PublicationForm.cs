using SystemyWP.Data.Enums;

namespace SystemyWP.API.Forms.Portal
{
    public class PublicationForm
    {
        public string Title { get; set; }
        public string News { get; set; }
        
        public string Image { get; set; }
        
        public PortalPublicationCategory PortalPublicationCategory { get; set; }
    }
}