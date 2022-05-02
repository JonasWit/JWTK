using System.Linq;
using AutoMapper;
using SystemyWP.API.Data.DTOs.Gastronomy.Dishes;
using SystemyWP.API.Data.DTOs.Gastronomy.Menus;
using SystemyWP.API.Services.HttpServices;

namespace SystemyWP.API.Profiles;

public class GastronomyServiceProfile : Profile
{
    public GastronomyServiceProfile(UrlService urlService)
    {
        CreateMap<MenuServiceDto, MenuDto>()
            .ForMember(dest => dest.Dishes, opt => opt.MapFrom(src => src.Dishes.Select(i => urlService.DishPath("GastronomyDishes", i))));
        
        CreateMap<DishServiceDto, DishDto>()
            .ForMember(dest => dest.Ingredients,opt => opt.MapFrom(src => src.Ingredients.Select(i => urlService.DishPath("GastronomyIngredients", i))))
            .ForMember(dest => dest.Menus, opt => opt.MapFrom(src => src.Menus.Select(i => urlService.DishPath("GastronomyMenus", i))));    
        
    }
    
    
    
    
}