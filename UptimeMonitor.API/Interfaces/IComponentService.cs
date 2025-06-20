using UptimeMonitor.API.DTOs;

namespace UptimeMonitor.API.Interfaces
{
    public interface IComponentService
    {
        Task<IEnumerable<ComponentResponseDto>> GetAllAsync();
        Task<ComponentResponseDto?> GetByIdAsync(int id);
        Task<ComponentResponseDto> CreateAsync(ComponentCreateDto dto);
        Task<ComponentResponseDto?> UpdateAsync(int id, ComponentCreateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ComponentResponseDto>> GetByServiceSystemIdAsync(int serviceSystemId);
    }
}
