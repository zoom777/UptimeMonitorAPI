using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UptimeMonitor.API.Data;
using UptimeMonitor.API.DTOs;
using UptimeMonitor.API.Entities;
using UptimeMonitor.API.Services;
using Xunit;

namespace UptimeMonitor.Tests.Services
{
    public class ServiceSystemServiceTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: $"UptimeTestDb_{Guid.NewGuid()}")
                .Options;

            var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllServiceSystems()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            context.ServiceSystems.Add(new ServiceSystem { Name = "System A", Description = "Desc A", Status = true });
            context.ServiceSystems.Add(new ServiceSystem { Name = "System B", Description = "Desc B", Status = false });
            await context.SaveChangesAsync();

            var service = new ServiceSystemService(context);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectServiceSystem()
        {
            var context = GetInMemoryDbContext();
            var entity = new ServiceSystem { Name = "System X", Description = "Desc X", Status = true };
            context.ServiceSystems.Add(entity);
            await context.SaveChangesAsync();

            var service = new ServiceSystemService(context);

            var result = await service.GetByIdAsync(entity.Id);

            Assert.NotNull(result);
            Assert.Equal("System X", result!.Name);
        }

        [Fact]
        public async Task CreateAsync_AddsNewServiceSystem()
        {
            var context = GetInMemoryDbContext();
            var service = new ServiceSystemService(context);

            var dto = new ServiceSystemCreateDto { Name = "New System", Description = "New Desc" };
            var result = await service.CreateAsync(dto);

            Assert.NotNull(result);
            Assert.Equal("New System", result.Name);

            var stored = await context.ServiceSystems.FirstOrDefaultAsync(s => s.Id == result.Id);
            Assert.NotNull(stored);
            Assert.Equal("New Desc", stored.Description);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesExistingServiceSystem()
        {
            var context = GetInMemoryDbContext();
            var entity = new ServiceSystem { Name = "Old Name", Description = "Old Desc", Status = true };
            context.ServiceSystems.Add(entity);
            await context.SaveChangesAsync();

            var service = new ServiceSystemService(context);
            var updatedDto = new ServiceSystemCreateDto { Name = "Updated Name", Description = "Updated Desc" };

            var result = await service.UpdateAsync(entity.Id, updatedDto);

            Assert.True(result);
            var updated = await context.ServiceSystems.FindAsync(entity.Id);
            Assert.Equal("Updated Name", updated!.Name);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsFalseIfNotFound()
        {
            var context = GetInMemoryDbContext();
            var service = new ServiceSystemService(context);

            var dto = new ServiceSystemCreateDto { Name = "Name", Description = "Desc" };
            var result = await service.UpdateAsync(999, dto);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_RemovesServiceSystem()
        {
            var context = GetInMemoryDbContext();
            var entity = new ServiceSystem { Name = "To Delete", Description = "Temp", Status = true };
            context.ServiceSystems.Add(entity);
            await context.SaveChangesAsync();

            var service = new ServiceSystemService(context);
            var result = await service.DeleteAsync(entity.Id);

            Assert.True(result);
            Assert.Null(await context.ServiceSystems.FindAsync(entity.Id));
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalseIfNotFound()
        {
            var context = GetInMemoryDbContext();
            var service = new ServiceSystemService(context);

            var result = await service.DeleteAsync(999);

            Assert.False(result);
        }
    }
}