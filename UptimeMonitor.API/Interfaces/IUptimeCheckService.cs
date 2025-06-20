using UptimeMonitor.API.DTOs;

namespace UptimeMonitor.API.Interfaces
{
    public interface IUptimeCheckService
    {
        Task<IEnumerable<UptimeCheckResponseDto>> GetAllAsync();
        Task<UptimeCheckResponseDto?> GetByIdAsync(int id);
        Task<UptimeCheckResponseDto> CreateAsync(UptimeCheckCreateDto dto);
        Task<UptimeCheckResponseDto?> UpdateAsync(int id, UptimeCheckCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
