using AutoMapper;
using SystemyWP.API.Gastronomy.Data.Models.Dishes;
using SystemyWP.API.Gastronomy.Data.Models.Menus;

namespace SystemyWP.API.Gastronomy.Profiles;

public class RestaurantAppProfile : Profile
{
    public RestaurantAppProfile()
    {
        // CreateMap<RestaurantAppMenu, MenuDto>()
        //     .ForMember(
        //         s => s.RestaurantAppDishes, 
        //         c => c.MapFrom(m => m.RestaurantAppDishes));
        // CreateMap<RestaurantAppDish, RestaurantAppDishDto>();
    }
}