using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UptimeMonitor.API.Data;
using UptimeMonitor.API.DTOs;
using UptimeMonitor.API.Entities;
using UptimeMonitor.API.Interfaces;

namespace UptimeMonitor.API.Services
{
    public class ComponentService : IComponentService
    {
        private readonly AppDbContext _context;

        public ComponentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ComponentResponseDto>> GetAllAsync()
        {
            return await _context.Components
                .Select(c => new ComponentResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Status = c.Status,
                    ServiceSystemId = c.ServiceSystemId
                }).ToListAsync();
        }

        public async Task<ComponentResponseDto?> GetByIdAsync(int id)
        {
            var entity = await _context.Components.FindAsync(id);
            return entity == null ? null : new ComponentResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Status = entity.Status,
                ServiceSystemId = entity.ServiceSystemId
            };
        }

        public async Task<ComponentResponseDto> CreateAsync(ComponentCreateDto dto)
        {
            var entity = new Component
            {
                Name = dto.Name,
                Description = dto.Description,
                Status = true,
                ServiceSystemId = dto.ServiceSystemId
            };
            _context.Components.Add(entity);
            await _context.SaveChangesAsync();

            return new ComponentResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Status = entity.Status,
                ServiceSystemId = entity.ServiceSystemId
            };
        }
        public async Task<ComponentResponseDto?> UpdateAsync(int id, ComponentCreateDto dto)
        {
            var entity = await _context.Components.FindAsync(id);
            if (entity == null) return null;

            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.ServiceSystemId = dto.ServiceSystemId;
            await _context.SaveChangesAsync();

            return new ComponentResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Status = entity.Status,
                ServiceSystemId = entity.ServiceSystemId
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Components.FindAsync(id);
            if (entity == null) return false;

            _context.Components.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<ComponentResponseDto>> GetByServiceSystemIdAsync(int serviceSystemId)
        {
            return await _context.Components
                .Where(c => c.ServiceSystemId == serviceSystemId)
                .Select(c => new ComponentResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Status = c.Status,
                    ServiceSystemId = c.ServiceSystemId
                })
                .ToListAsync();
        }
    }
}
