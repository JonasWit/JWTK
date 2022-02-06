using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SystemyWP.API.Controllers;

[Route("[controller]")]
public class HealthController : ApiControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ILogger<HealthController> _logger;

    public HealthController(
        IWebHostEnvironment webHostEnvironment,
        ILogger<HealthController> logger)
    {
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;
    }
    
    [DisableCors]
    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogWarning($"Health Check endpoint called!");
        return Ok(new {Message= "Service is alive!" });
    }
}