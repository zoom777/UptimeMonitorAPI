using Microsoft.EntityFrameworkCore;
using UptimeMonitor.API.Data;
using UptimeMonitor.API.DTOs;
using UptimeMonitor.API.Entities;
using UptimeMonitor.API.Interfaces;

namespace UptimeMonitor.API.Services
{
    public class ServiceSystemService : IServiceSystemService
    {
        private readonly AppDbContext _context;

        public ServiceSystemService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceSystemResponseDto>> GetAllAsync()
        {
            return await _context.ServiceSystems
                .Select(s => new ServiceSystemResponseDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Status = s.Status,
                }).ToListAsync();
        }

        public async Task<ServiceSystemResponseDto?> GetByIdAsync(int id)
        {
            var entity = await _context.ServiceSystems.FindAsync(id);
            return entity == null ? null : new ServiceSystemResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Status = entity.Status,
            };
        }

        public async Task<ServiceSystemResponseDto> CreateAsync(ServiceSystemCreateDto dto)
        {
            var entity = new ServiceSystem
            {
                Name = dto.Name,
                Description = dto.Description,
                Status = true
            };
            _context.ServiceSystems.Add(entity);
            await _context.SaveChangesAsync();

            return new ServiceSystemResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Status = entity.Status
            };
        }

        public async Task<bool> UpdateAsync(int id, ServiceSystemCreateDto dto)
        {
            var entity = await _context.ServiceSystems.FindAsync(id);
            if (entity == null) return false;

            entity.Name = dto.Name;
            entity.Description = dto.Description;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ServiceSystems.FindAsync(id);
            if (entity == null) return false;

            _context.ServiceSystems.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
