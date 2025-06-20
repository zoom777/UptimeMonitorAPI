using UptimeMonitor.API.DTOs;

namespace UptimeMonitor.API.Interfaces
{
    public interface IUptimeEventService
    {
        Task<IEnumerable<UptimeEventResponseDto>> GetAllAsync();
        Task<IEnumerable<UptimeEventExportDto>> GetAllForExportAsync(DateTime? dateFrom, DateTime? dateTo);
        Task<byte[]> GenerateExcelAsync(DateTime? dateFrom, DateTime? dateTo);
        Task<UptimeEventUpdateResponseDto?> UpdatePartialAsync(int id, UptimeEventUpdateDto dto);
    }
}