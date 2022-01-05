using Microsoft.AspNetCore.Mvc;

namespace SystemyWP.API.Gastronomy.Controllers;

[Route("/api/gastro/[controller]")]
[ApiController]
public class LiveController : ControllerBase
{
    [HttpGet]
    public IActionResult CheckIsAlive() => Ok("I`m alive!");
}