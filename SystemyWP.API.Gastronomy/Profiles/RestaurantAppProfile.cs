using System.Linq;
using AutoMapper;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.DTOs;
using SystemyWP.API.Gastronomy.DTOs.DishDTOs;
using SystemyWP.API.Gastronomy.DTOs.IngredientDTOs;

namespace SystemyWP.API.Gastronomy.Profiles;

public class RestaurantAppProfile : Profile
{
    public RestaurantAppProfile()
    {
        CreateMap<IngredientCreateDto, Ingredient>();
        CreateMap<Ingredient, IngredientDto>();
        CreateMap<IngredientDto, Ingredient>();
        
        CreateMap<DishCreateDto, Dish>();
        CreateMap<Dish, DishDto>();
        CreateMap<Dish, DishDto>()
            .ForMember(dest => dest.Ingredients,opt => opt.MapFrom(src => src.Ingredients.Select(i => i.Id)))
            .ForMember(dest => dest.Menus, opt => opt.MapFrom(src => src.Menus.Select(i => i.Id)));       
        
        CreateMap<DishDto, Dish>();
        CreateMap<DishBasicDto, Dish>();

        CreateMap<DishIngredientUpdateDto, ResourceAccessPass>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DishId));
    }
}