using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SystemyWP.API.DTOs;
using SystemyWP.API.HttpClients;

namespace SystemyWP.API.Controllers;

[Route("[controller]")]
public class HealthController : ApiControllerBase
{
    private readonly GastronomyHttpClient _gastronomyHttpClient;
    private readonly ILogger<HealthController> _logger;

    public HealthController(
        GastronomyHttpClient gastronomyHttpClient,
        ILogger<HealthController> logger)
    {
        _gastronomyHttpClient = gastronomyHttpClient;
        _logger = logger;
    }
    
    [DisableCors]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        _logger.LogWarning($"Health Check endpoint called!");
        var res = new HealthCheckDto
        {
            MasterServiceStatus = SystemyWpConstants.ServiceResponses.AliveResponse,
            GastronomyServiceStatus = await _gastronomyHttpClient.GetHealthCheckResponse()
        };

        return Ok(res);
    }
}