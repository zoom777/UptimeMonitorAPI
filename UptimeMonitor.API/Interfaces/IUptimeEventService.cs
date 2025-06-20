using UptimeMonitor.API.DTOs;

namespace UptimeMonitor.API.Interfaces
{
    public interface IUptimeEventService
    {
        Task<IEnumerable<UptimeEventResponseDto>> GetAllAsync();
    }
}