using Microsoft.AspNetCore.Mvc;

namespace tschiraplusAPI.Controllers;

[ApiController]
[Route("")]
public class HealthCheckController : ControllerBase
{
    [HttpGet]
    public IActionResult GetHealthStatus()
    {
        return Ok();
    }
}