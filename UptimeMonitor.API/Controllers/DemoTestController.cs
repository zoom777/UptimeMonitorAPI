using Microsoft.AspNetCore.Mvc;

namespace UptimeMonitor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DemoTestController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<string>> GetDemo() =>
            Ok("Demo testing...1.2.3.");
    }
}
