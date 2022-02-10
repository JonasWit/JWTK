using Microsoft.AspNetCore.Mvc;

namespace SystemyWP.API.Gastronomy.Controllers;

[Route("[controller]")]
[ApiController]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok();
}