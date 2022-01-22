using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Data;
using SystemyWP.API.Data.Models.General;

namespace SystemyWP.API.Controllers.Logs;

[Route("api/[controller]")]
public class LogsController : ApiControllerBase
{
    private readonly ILogger<LogsController> _logger;
    private readonly AppDbContext _context;

    public LogsController(
        ILogger<LogsController> logger,
        AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    [HttpGet("test")]
    public ActionResult<Log> Test()
    {
        try
        {
            _logger.LogWarning("TEST");

            var res = _context.Logs.ToList(); 
                
            return Ok(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(SystemyWpConstants.ExceptionConsoleMessage(e));
            return null;
        }
    }
}