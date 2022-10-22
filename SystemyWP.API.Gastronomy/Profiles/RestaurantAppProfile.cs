using System.Linq;
using AutoMapper;
using MasterService.API.Gastronomy.Data.DTOs;
using MasterService.API.Gastronomy.Data.DTOs.DishDTOs;
using MasterService.API.Gastronomy.Data.DTOs.IngredientDTOs;
using MasterService.API.Gastronomy.Data.DTOs.MenuDTOs;
using MasterService.API.Gastronomy.Data.Models;

namespace MasterService.API.Gastronomy.Profiles;

public class RestaurantAppProfile : Profile
{
    public RestaurantAppProfile()
    {
        // Ingredient
        CreateMap<IngredientCreateDto, Ingredient>();
        CreateMap<Ingredient, IngredientDto>();
        CreateMap<IngredientDto, Ingredient>();
        
        // Dish
        CreateMap<DishCreateDto, Dish>();
        CreateMap<DishDto, Dish>();
        CreateMap<DishBasicDto, Dish>();    
        
        CreateMap<Dish, DishDto>()
            .ForMember(dest => dest.Ingredients,opt => opt.MapFrom(src => src.Ingredients.Select(i => i.Id)))
            .ForMember(dest => dest.Menus, opt => opt.MapFrom(src => src.Menus.Select(i => i.Id)));       
        
        CreateMap<DishIngredientUpdateDto, ResourceAccessPass>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DishId));
        
        // Menu
        CreateMap<MenuCreateDto, Menu>();
        CreateMap<MenuDto, Menu>();
        CreateMap<MenuBasicDto, Menu>();           
        
        CreateMap<Menu, MenuDto>()
            .ForMember(dest => dest.Dishes, opt => opt.MapFrom(src => src.Dishes.Select(i => i.Id)));
        
        CreateMap<MenuDishUpdateDto, ResourceAccessPass>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MenuId));
    }
}