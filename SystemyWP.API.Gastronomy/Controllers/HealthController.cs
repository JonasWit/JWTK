using Microsoft.AspNetCore.Mvc;

namespace SystemyWP.API.Gastronomy.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok();
}