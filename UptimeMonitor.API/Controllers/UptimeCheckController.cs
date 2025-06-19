using Microsoft.AspNetCore.Mvc;
using UptimeMonitor.API.DTOs;
using UptimeMonitor.API.Interfaces;

namespace UptimeMonitor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UptimeCheckController : ControllerBase
    {
        private readonly IUptimeCheckService _service;

        public UptimeCheckController(IUptimeCheckService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UptimeCheckResponseDto>>> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<UptimeCheckResponseDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<UptimeCheckResponseDto>> Create(UptimeCheckCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UptimeCheckCreateDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
