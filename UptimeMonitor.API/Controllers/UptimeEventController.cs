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
        public async Task<ActionResult<IEnumerable<UptimeEventResponseDto>>> GetAll(
            [FromQuery] int? systemId,
            [FromQuery] int? componentId,
            [FromQuery] DateTime? startTime,
            [FromQuery] DateTime? endTime,
            [FromQuery] bool? status,
            [FromQuery] bool? isFalsePositive)
        {
            var events = await _service.GetAllAsync(systemId, componentId, startTime, endTime, status, isFalsePositive);
            return Ok(events);
        }

        [HttpPut("{id}/partial")]
        public async Task<IActionResult> UpdatePartial(int id, [FromBody] UptimeEventUpdateDto dto)
        {
            var result = await _service.UpdatePartialAsync(id, dto);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportToExcel([FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo)
        {
            var fileContent = await _service.GenerateExcelAsync(dateFrom, dateTo);

            return File(
                fileContent,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"uptime-events-{DateTime.UtcNow:yyyyMMddHHmmss}.xlsx"
            );
        }
    }
}