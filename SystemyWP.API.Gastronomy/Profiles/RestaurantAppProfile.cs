using AutoMapper;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.Lib.Shared.DTOs.Gastronomy;

namespace SystemyWP.API.Gastronomy.Profiles;

public class RestaurantAppProfile : Profile
{
    public RestaurantAppProfile()
    {
        CreateMap<CreateIngredientDto, Ingredient>();
        CreateMap<Ingredient, IngredientDto>();
    }
}