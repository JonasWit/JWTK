using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SystemyWP.API.Controllers;

[Route("[controller]")]
public class HealthController : ApiControllerBase
{
    private readonly ILogger<HealthController> _logger;

    public HealthController(ILogger<HealthController> logger)
    {
        _logger = logger;
    }
    
    [DisableCors]
    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogWarning($"Health Check endpoint called!");
        return Ok(new {MasterStatus = "Service is alive!" });
    }
}