using Microsoft.EntityFrameworkCore;
using UptimeMonitor.API.Data;
using UptimeMonitor.API.DTOs;
using UptimeMonitor.API.Entities;
using UptimeMonitor.API.Services;

namespace UptimeMonitor.Tests.Services
{
    public class ComponentServiceTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"UptimeTestDb_Component_{Guid.NewGuid()}")
                .Options;

            var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllComponents()
        {
            var context = GetInMemoryDbContext();
            context.Components.AddRange(
                new Component { Name = "Component A", Description = "Desc A", Status = true, ServiceSystemId = 1 },
                new Component { Name = "Component B", Description = "Desc B", Status = true, ServiceSystemId = 2 }
            );
            await context.SaveChangesAsync();

            var service = new ComponentService(context);
            var result = await service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectComponent()
        {
            var context = GetInMemoryDbContext();
            var component = new Component { Name = "Component X", Description = "Desc X", Status = true, ServiceSystemId = 1 };
            context.Components.Add(component);
            await context.SaveChangesAsync();

            var service = new ComponentService(context);
            var result = await service.GetByIdAsync(component.Id);

            Assert.NotNull(result);
            Assert.Equal("Component X", result!.Name);
        }

        [Fact]
        public async Task CreateAsync_AddsNewComponent()
        {
            var context = GetInMemoryDbContext();
            var service = new ComponentService(context);

            var dto = new ComponentCreateDto
            {
                Name = "New Component",
                Description = "New Description",
                ServiceSystemId = 3
            };

            var result = await service.CreateAsync(dto);

            Assert.NotNull(result);
            Assert.Equal("New Component", result.Name);

            var stored = await context.Components.FindAsync(result.Id);
            Assert.NotNull(stored);
            Assert.Equal("New Description", stored!.Description);
            Assert.True(stored.Status);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesExistingComponent()
        {
            var context = GetInMemoryDbContext();
            var component = new Component { Name = "Old", Description = "Old", Status = true, ServiceSystemId = 1 };
            context.Components.Add(component);
            await context.SaveChangesAsync();

            var service = new ComponentService(context);

            var dto = new ComponentCreateDto
            {
                Name = "Updated",
                Description = "Updated Desc",
                ServiceSystemId = 5
            };

            var result = await service.UpdateAsync(component.Id, dto);

            Assert.NotNull(result);
            Assert.Equal("Updated", result!.Name);
            Assert.Equal("Updated Desc", result.Description);
            Assert.Equal(5, result.ServiceSystemId);

            var updated = await context.Components.FindAsync(component.Id);
            Assert.Equal("Updated", updated!.Name);
            Assert.Equal("Updated Desc", updated.Description);
            Assert.Equal(5, updated.ServiceSystemId);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsNullIfNotFound()
        {
            var context = GetInMemoryDbContext();
            var service = new ComponentService(context);

            var dto = new ComponentCreateDto { Name = "Test", Description = "Test Desc", ServiceSystemId = 1 };
            var result = await service.UpdateAsync(999, dto);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_DeletesComponent()
        {
            var context = GetInMemoryDbContext();
            var component = new Component { Name = "ToDelete", Description = "Test", Status = true, ServiceSystemId = 1 };
            context.Components.Add(component);
            await context.SaveChangesAsync();

            var service = new ComponentService(context);
            var result = await service.DeleteAsync(component.Id);

            Assert.True(result);
            Assert.Null(await context.Components.FindAsync(component.Id));
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalseIfNotFound()
        {
            var context = GetInMemoryDbContext();
            var service = new ComponentService(context);

            var result = await service.DeleteAsync(999);

            Assert.False(result);
        }
    }
}
