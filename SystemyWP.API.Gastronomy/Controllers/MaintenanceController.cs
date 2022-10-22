using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using MasterService.API.Gastronomy.Data.Repositories.RepositoriesInterfaces;

namespace MasterService.API.Gastronomy.Controllers;

[ApiController]
[Route("[controller]")]
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
            _ = await _maintenanceRepository.SaveChanges();
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(RemoveData)} Error");
            return Problem($"{nameof(RemoveData)} Error");
        }
    }
}