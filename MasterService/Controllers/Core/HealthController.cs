using Domain.MasterServiceShared.DTOs;
using MasterService.API.Constants;
using MasterService.Data.Repositories;
using MasterService.Services.HttpServices;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MasterService.Controllers.Core;

[Route("[controller]")]
public class HealthController : ApiControllerBase
{
    private readonly UserRepository _userRepository;
    private readonly GastronomyHttpClient _gastronomyHttpClient;
    private readonly ILogger<HealthController> _logger;

    public HealthController(
        UserRepository userRepository,
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
                MasterServiceStatus = AppConstants.ServiceResponses.AliveResponse,
                GastronomyServiceStatus = await _gastronomyHttpClient.GetHealthCheckResponse()
            };

            return Ok(res);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Issue during Health check");
            return Problem(AppConstants.ResponseMessages.DefaultExceptionMessage);
        }
    }
}