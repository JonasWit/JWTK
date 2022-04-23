using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SystemyWP.API.Constants;
using SystemyWP.API.Services.HttpClients;

namespace SystemyWP.API.Controllers.MasterService;

[Authorize]
[Route("[controller]")]
public class LogsController : ApiControllerBase
{
    private readonly ILogger<LogsController> _logger;

    public LogsController(ILogger<LogsController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok("test");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "");
            return Problem(AppConstants.ResponseMessages.DefaultExceptionMessage);
        }
    }
    
    
}