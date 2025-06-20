using Microsoft.AspNetCore.Mvc;
using UptimeMonitor.API.DTOs;
using UptimeMonitor.API.Interfaces;

namespace UptimeMonitor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UptimeEventController : ControllerBase
    {
        private readonly IUptimeEventService _service;

        public UptimeEventController(IUptimeEventService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UptimeEventResponseDto>>> GetAll()
        {
            var events = await _service.GetAllAsync();
            return Ok(events);
        }
    }
}