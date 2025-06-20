using UptimeMonitor.API.DTOs;

namespace UptimeMonitor.API.Interfaces
{
    public interface IUptimeEventService
    {
        Task<IEnumerable<UptimeEventResponseDto>> GetAllAsync();
        Task<IEnumerable<UptimeEventExportDto>> GetAllForExportAsync();
        Task<byte[]> GenerateExcelAsync();
        Task<UptimeEventUpdateResponseDto?> UpdatePartialAsync(int id, UptimeEventUpdateDto dto);
    }
}