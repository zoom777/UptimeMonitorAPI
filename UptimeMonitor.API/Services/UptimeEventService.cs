using Microsoft.EntityFrameworkCore;
using UptimeMonitor.API.Data;
using UptimeMonitor.API.DTOs;
using UptimeMonitor.API.Interfaces;

namespace UptimeMonitor.API.Services
{
    public class UptimeEventService : IUptimeEventService
    {
        private readonly AppDbContext _context;

        public UptimeEventService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UptimeEventResponseDto>> GetAllAsync()
        {
            return await _context.UptimeEvents
                .AsNoTracking()
                .Select(e => new UptimeEventResponseDto
                {
                    Id = e.Id,
                    UptimeCheckId = e.UptimeCheckId,
                    StartTime = e.StartTime,
                    EndTime = e.EndTime,
                    IsUp = e.IsUp,
                    IsFalsePositive = e.IsFalsePositive,
                    Category = e.Category,
                    Note = e.Note,
                    JiraTicket = e.JiraTicket,
                    MaintenanceType = e.MaintenanceType
                })
                .ToListAsync();
        }
    }
}