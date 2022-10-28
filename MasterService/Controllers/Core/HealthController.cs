using Domain.MasterServiceShared.DTOs;
using MasterService.API.Constants;
using MasterService.Data.Repositories;
using MasterService.Services.HttpServices;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MasterService.Controllers.Core;

[Route("[controller]")]
public class HealthController : ApiControllerBase
{
    private readonly UserRepository _userRepository;
    private readonly GastronomyHttpClient _gastronomyHttpClient;

    public HealthController(
        UserRepository userRepository,
        GastronomyHttpClient gastronomyHttpClient)
    {
        _userRepository = userRepository;
        _gastronomyHttpClient = gastronomyHttpClient;
    }

    [DisableCors]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var res = new HealthCheckDto
        {
            MasterServiceStatus = AppConstants.ServiceResponses.AliveResponse,
            GastronomyServiceStatus = await _gastronomyHttpClient.GetHealthCheckResponse()
        };

        return Ok(res);
    }
}