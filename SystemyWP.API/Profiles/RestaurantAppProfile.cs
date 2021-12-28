using AutoMapper;
using SystemyWP.API.Data.Models.RestaurantAppModels.Dishes;
using SystemyWP.API.Data.Models.RestaurantAppModels.Menus;
using SystemyWP.API.DTOs.RestaurantApp;

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