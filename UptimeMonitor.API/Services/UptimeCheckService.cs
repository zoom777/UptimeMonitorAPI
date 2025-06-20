using Microsoft.EntityFrameworkCore;
using UptimeMonitor.API.Data;
using UptimeMonitor.API.DTOs;
using UptimeMonitor.API.Entities;
using UptimeMonitor.API.Interfaces;

namespace UptimeMonitor.API.Services
{
    public class UptimeCheckService : IUptimeCheckService
    {
        private readonly AppDbContext _context;

        public UptimeCheckService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UptimeCheckResponseDto>> GetAllAsync()
        {
            return await _context.UptimeChecks
                .Select(c => new UptimeCheckResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    ServiceSystemId = c.ServiceSystemId,
                    ComponentId = c.ComponentId,
                    CheckUrl = c.CheckUrl,
                    CheckInterval = c.CheckInterval,
                    CheckTimeout = c.CheckTimeout,
                    RequestHeaders = c.RequestHeaders,
                    DownAlertDelay = c.DownAlertDelay,
                    DownAlertResend = c.DownAlertResend,
                    DownAlertMessage = c.DownAlertMessage,
                    AlertEmail = c.AlertEmail
                }).ToListAsync();
        }

        public async Task<UptimeCheckResponseDto?> GetByIdAsync(int id)
        {
            var entity = await _context.UptimeChecks.FindAsync(id);
            return entity == null ? null : new UptimeCheckResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                ServiceSystemId = entity.ServiceSystemId,
                ComponentId = entity.ComponentId,
                CheckUrl = entity.CheckUrl,
                CheckInterval = entity.CheckInterval,
                CheckTimeout = entity.CheckTimeout,
                RequestHeaders = entity.RequestHeaders,
                DownAlertDelay = entity.DownAlertDelay,
                DownAlertResend = entity.DownAlertResend,
                DownAlertMessage = entity.DownAlertMessage,
                AlertEmail = entity.AlertEmail
            };
        }

        public async Task<UptimeCheckResponseDto> CreateAsync(UptimeCheckCreateDto dto)
        {
            var entity = new UptimeCheck
            {
                Name = dto.Name,
                ServiceSystemId = dto.ServiceSystemId,
                ComponentId = dto.ComponentId,
                CheckUrl = dto.CheckUrl,
                CheckInterval = dto.CheckInterval,
                CheckTimeout = dto.CheckTimeout,
                RequestHeaders = dto.RequestHeaders,
                DownAlertDelay = dto.DownAlertDelay,
                DownAlertResend = dto.DownAlertResend,
                DownAlertMessage = dto.DownAlertMessage,
                AlertEmail = dto.AlertEmail
            };
            _context.UptimeChecks.Add(entity);
            await _context.SaveChangesAsync();

            return new UptimeCheckResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                ServiceSystemId = entity.ServiceSystemId,
                ComponentId = entity.ComponentId,
                CheckUrl = entity.CheckUrl,
                CheckInterval = entity.CheckInterval,
                CheckTimeout = entity.CheckTimeout,
                RequestHeaders = entity.RequestHeaders,
                DownAlertDelay = entity.DownAlertDelay,
                DownAlertResend = entity.DownAlertResend,
                DownAlertMessage = entity.DownAlertMessage,
                AlertEmail = entity.AlertEmail
            };
        }

        public async Task<UptimeCheckResponseDto?> UpdateAsync(int id, UptimeCheckCreateDto dto)
        {
            var entity = await _context.UptimeChecks.FindAsync(id);
            if (entity == null) return null;

            entity.Name = dto.Name;
            entity.ServiceSystemId = dto.ServiceSystemId;
            entity.ComponentId = dto.ComponentId;
            entity.CheckUrl = dto.CheckUrl;
            entity.CheckInterval = dto.CheckInterval;
            entity.CheckTimeout = dto.CheckTimeout;
            entity.RequestHeaders = dto.RequestHeaders;
            entity.DownAlertDelay = dto.DownAlertDelay;
            entity.DownAlertResend = dto.DownAlertResend;
            entity.DownAlertMessage = dto.DownAlertMessage;
            entity.AlertEmail = dto.AlertEmail;

            await _context.SaveChangesAsync();

            return new UptimeCheckResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                ServiceSystemId = entity.ServiceSystemId,
                ComponentId = entity.ComponentId,
                CheckUrl = entity.CheckUrl,
                CheckInterval = entity.CheckInterval,
                CheckTimeout = entity.CheckTimeout,
                RequestHeaders = entity.RequestHeaders,
                DownAlertDelay = entity.DownAlertDelay,
                DownAlertResend = entity.DownAlertResend,
                DownAlertMessage = entity.DownAlertMessage,
                AlertEmail = entity.AlertEmail
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.UptimeChecks.FindAsync(id);
            if (entity == null) return false;

            _context.UptimeChecks.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
