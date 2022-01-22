using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SystemyWP.API.Controllers.BaseClases;
using SystemyWP.API.Data;
using SystemyWP.API.Data.Models.General;
using SystemyWP.API.Data.Models.UsersManagement;
using SystemyWP.API.DTOs;
using SystemyWP.API.Forms.User;
using SystemyWP.API.Repositories.General;
using SystemyWP.API.Services.Auth;
using SystemyWP.API.Settings;

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