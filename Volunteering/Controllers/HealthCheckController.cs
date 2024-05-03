using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Volunteering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult CheckHealth([FromServices] HealthCheckService healthCheckService)
        {
            var report = healthCheckService.CheckHealthAsync(new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token);
            if (report.Result.Status == HealthStatus.Healthy)
            {
                return Ok(new { Status = "Healthy", Timestamp = DateTime.UtcNow });
            }

            return StatusCode(503, new { Status = "Server unavailable", Timestamp = DateTime.UtcNow });
        }
    }
}
