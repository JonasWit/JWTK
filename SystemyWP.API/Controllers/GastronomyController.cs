using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SystemyWP.API.Settings;

namespace SystemyWP.API.Controllers;

[Route("[controller]")]
public class GastronomyController : ApiControllerBase
{

    private readonly ILogger<GastronomyController> _logger;
    private readonly IOptionsMonitor<AuthSettings> _optionsMonitor;

    public GastronomyController(
        ILogger<GastronomyController> logger,
        IOptionsMonitor<AuthSettings> optionsMonitor)
    {
        _logger = logger;
        _optionsMonitor = optionsMonitor;
    }
    
    [Authorize]
    [HttpGet("authorized-check")]
    public IActionResult AuthorizedCheck() => Ok(new {Message= "Authorized Response", UserEmail, UserId });
    
    
    
    
}