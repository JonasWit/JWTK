using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SystemyWP.API.HttpClients;

namespace SystemyWP.API.Controllers;

[Authorize]
[Route("[controller]")]
public class GastronomyController : ApiControllerBase
{
    private readonly GastronomyHttpClient _gastronomyHttpClient;
    private readonly ILogger<GastronomyController> _logger;

    public GastronomyController(
        GastronomyHttpClient gastronomyHttpClient,
        ILogger<GastronomyController> logger)
    {
        _gastronomyHttpClient = gastronomyHttpClient;
        _logger = logger;
    }
    

    
    
    
    
    
    
    
}