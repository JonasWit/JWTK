using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SystemyWP.API.Gastronomy.Repositories;

namespace SystemyWP.API.Gastronomy.Controllers;

[ApiController]
[Route("[controller]")]
public class DishController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IIngredientRepository _ingredientRepository;
    private readonly ILogger<DishController> _logger;
    
    public DishController(
        IMapper mapper,
        IIngredientRepository ingredientRepository,
        ILogger<DishController> logger)
    {
        _mapper = mapper;
        _ingredientRepository = ingredientRepository;
        _logger = logger;
    }
    
    
    
    
    
    
}