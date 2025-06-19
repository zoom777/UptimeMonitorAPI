using UptimeMonitor.API.DTOs;

namespace UptimeMonitor.API.Interfaces
{
    public interface IServiceSystemService
    {
        Task<IEnumerable<ServiceSystemResponseDto>> GetAllAsync();
        Task<ServiceSystemResponseDto?> GetByIdAsync(int id);
        Task<ServiceSystemResponseDto> CreateAsync(ServiceSystemCreateDto dto);
        Task<bool> UpdateAsync(int id, ServiceSystemCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
