using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SystemyWP.API.Gastronomy.Repositories.RepositoriesInterfaces;

namespace SystemyWP.API.Gastronomy.Controllers;

public class MaintenanceController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMaintenanceRepository _maintenanceRepository;
    private readonly ILogger<MaintenanceController> _logger;

    public MaintenanceController(
        IMapper mapper,
        IMaintenanceRepository maintenanceRepository,
        ILogger<MaintenanceController> logger)
    {
        _mapper = mapper;
        _maintenanceRepository = maintenanceRepository;
        _logger = logger;
    }
    
    [HttpDelete("{key}", Name = "RemoveData")]
    public async Task<IActionResult> RemoveData(string key)
    {
        try
        {
            _maintenanceRepository.RemoveAllData(key);
            if (await _maintenanceRepository.SaveChanges() > 0) return Ok();    
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e, AppConstants.ResponseMessages.MaintenanceRemoveData);
            return Problem(AppConstants.ResponseMessages.MaintenanceRemoveData);
        }
    }
    
    
    
    
    
    
    
    
}