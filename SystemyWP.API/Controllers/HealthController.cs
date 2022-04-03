using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SystemyWP.API.Constants;
using SystemyWP.API.DTOs;
using SystemyWP.API.DTOs.General;
using SystemyWP.API.HttpClients;
using SystemyWP.API.Repositories;

namespace SystemyWP.API.Controllers;

[Route("[controller]")]
public class HealthController : ApiControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly GastronomyHttpClient _gastronomyHttpClient;
    private readonly ILogger<HealthController> _logger;

    public HealthController(
        IUserRepository userRepository,
        GastronomyHttpClient gastronomyHttpClient,
        ILogger<HealthController> logger)
    {
        _userRepository = userRepository;
        _gastronomyHttpClient = gastronomyHttpClient;
        _logger = logger;
    }
    
    [DisableCors]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            _logger.LogWarning($"Health Check endpoint called!");
            var res = new HealthCheckDto
            {
                MasterServiceStatus = SystemyWpConstants.ServiceResponses.AliveResponse,
                GastronomyServiceStatus = await _gastronomyHttpClient.GetHealthCheckResponse()
            };

            return Ok(res);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Issue during Health check");
            return ServerError;
        }
    }
}