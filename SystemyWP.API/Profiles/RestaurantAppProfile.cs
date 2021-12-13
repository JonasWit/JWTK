using AutoMapper;
using SystemyWP.API.Dtos.Restaurant;
using SystemyWP.Data.Models.RestaurantAppModels.Dishes;
using SystemyWP.Data.Models.RestaurantAppModels.Menus;

namespace SystemyWP.API.Profiles;

public class RestaurantAppProfile : Profile
{
    public RestaurantAppProfile()
    {
        CreateMap<RestaurantAppMenu, MenuDto>()
            .ForMember(
                s => s.RestaurantAppDishes, 
                c => c.MapFrom(m => m.RestaurantAppDishes));
        CreateMap<RestaurantAppDish, RestaurantAppDishDto>();





    }
}