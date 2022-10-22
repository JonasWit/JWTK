using Microsoft.AspNetCore.Mvc;

namespace MasterService.API.Gastronomy.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok();
}