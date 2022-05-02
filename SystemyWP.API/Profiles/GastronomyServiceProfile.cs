using AutoMapper;
using SystemyWP.API.Data.DTOs.Gastronomy.Menus;
using SystemyWP.API.Services.HttpServices;

namespace SystemyWP.API.Profiles;

public class GastronomyServiceProfile : Profile
{
    private readonly UrlService _urlService;
    
    public GastronomyServiceProfile( UrlService urlService)
    {
        _urlService = urlService;
        

    }
    
    
    
    
}