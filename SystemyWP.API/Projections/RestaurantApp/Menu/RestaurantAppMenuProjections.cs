using System;
using System.Linq.Expressions;
using SystemyWP.Data.Models.RestaurantAppModels.Menus;

namespace SystemyWP.API.Projections.RestaurantApp.Menu
{
    public class RestaurantAppMenuProjections
    {
        public static Func<RestaurantAppMenu, object> CreateFlat => FlatProjection.Compile();
        public static Expression<Func<RestaurantAppMenu, object>> FlatProjection =>
            menu => new
            {
                menu.Created,
                menu.CreatedBy,
                menu.Updated,
                menu.UpdatedBy,
                menu.Id,
                menu.Active,
                menu.Name,
            };
    }
}