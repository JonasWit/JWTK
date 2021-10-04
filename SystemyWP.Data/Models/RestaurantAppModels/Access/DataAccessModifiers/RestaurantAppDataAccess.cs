using SystemyWP.Data.Models.Abstractions;
using SystemyWP.Data.Models.General;

namespace SystemyWP.Data.Models.RestaurantAppModels.Access.DataAccessModifiers
{
    public class RestaurantAppDataAccess : BaseModel<long>
    {
        public RestaurantAppRestrictedType RestaurantAppRestrictedType { get; set; }
        public long ItemId { get; set; }
        
        public User User { get; set; }
        public string UserId { get; set; }

        public RestaurantAccessKey RestaurantAccessKey { get; set; }
        public int RestaurantAccessKeyId { get; set; }
    }
}