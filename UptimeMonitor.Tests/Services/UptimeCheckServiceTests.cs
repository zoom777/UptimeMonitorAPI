using Microsoft.EntityFrameworkCore;
using UptimeMonitor.API.Data;
using UptimeMonitor.API.DTOs;
using UptimeMonitor.API.Entities;
using UptimeMonitor.API.Services;
using Xunit;

namespace UptimeMonitor.Tests.Services
{
    public class UptimeCheckServiceTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task CreateAsync_AddsNewUptimeCheck()
        {
            var context = GetInMemoryDbContext();
            var service = new UptimeCheckService(context);

            var dto = new UptimeCheckCreateDto
            {
                Name = "Check Google",
                ServiceSystemId = 1,
                ComponentId = 1,
                CheckUrl = "https://www.google.com",
                CheckInterval = 60,
                CheckTimeout = 10,
                RequestHeaders = "User-Agent: test",
                DownAlertDelay = 2,
                DownAlertResend = 5,
                DownAlertMessage = "Google is down",
                AlertEmail = "test@example.com"
            };

            var result = await service.CreateAsync(dto);

            Assert.NotNull(result);
            Assert.Equal(dto.Name, result.Name);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllItems()
        {
            var context = GetInMemoryDbContext();
            context.UptimeChecks.Add(new UptimeCheck
            {
                Name = "One",
                ServiceSystemId = 1,
                ComponentId = 1,
                CheckUrl = "http://test.com",
                CheckInterval = 30,
                CheckTimeout = 5,
                RequestHeaders = "Test",
                DownAlertDelay = 1,
                DownAlertResend = 2,
                DownAlertMessage = "Down!",
                AlertEmail = "one@example.com"
            });
            context.SaveChanges();

            var service = new UptimeCheckService(context);
            var result = await service.GetAllAsync();

            Assert.Single(result);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectItem()
        {
            var context = GetInMemoryDbContext();
            var entity = new UptimeCheck
            {
                Name = "Target",
                ServiceSystemId = 2,
                ComponentId = 2,
                CheckUrl = "https://target.com",
                CheckInterval = 60,
                CheckTimeout = 15,
                RequestHeaders = "Header: Value",
                DownAlertDelay = 3,
                DownAlertResend = 6,
                DownAlertMessage = "Down message",
                AlertEmail = "target@example.com"
            };
            context.UptimeChecks.Add(entity);
            await context.SaveChangesAsync();

            var service = new UptimeCheckService(context);
            var result = await service.GetByIdAsync(entity.Id);

            Assert.NotNull(result);
            Assert.Equal("Target", result!.Name);
        }

        [Fact]
        public async Task UpdateAsync_ChangesEntityValues()
        {
            var context = GetInMemoryDbContext();
            var entity = new UptimeCheck
            {
                Name = "ToUpdate",
                ServiceSystemId = 1,
                ComponentId = 1,
                CheckUrl = "http://old.com",
                CheckInterval = 60,
                CheckTimeout = 5,
                RequestHeaders = "",
                DownAlertDelay = 2,
                DownAlertResend = 4,
                DownAlertMessage = "Old message",
                AlertEmail = "old@example.com"
            };
            context.UptimeChecks.Add(entity);
            await context.SaveChangesAsync();

            var service = new UptimeCheckService(context);
            var updateDto = new UptimeCheckCreateDto
            {
                Name = "Updated",
                ServiceSystemId = 99,
                ComponentId = 99,
                CheckUrl = "https://updated.com",
                CheckInterval = 120,
                CheckTimeout = 10,
                RequestHeaders = "X-Updated: true",
                DownAlertDelay = 10,
                DownAlertResend = 20,
                DownAlertMessage = "Updated alert",
                AlertEmail = "updated@example.com"
            };

            var result = await service.UpdateAsync(entity.Id, updateDto);

            Assert.True(result);
            var updated = await context.UptimeChecks.FindAsync(entity.Id);
            Assert.Equal("Updated", updated!.Name);
            Assert.Equal("https://updated.com", updated.CheckUrl);
            Assert.Equal("updated@example.com", updated.AlertEmail);
        }


        [Fact]
        public async Task DeleteAsync_RemovesEntity()
        {
            var context = GetInMemoryDbContext();
            var entity = new UptimeCheck
            {
                Name = "ToDelete",
                ServiceSystemId = 1,
                ComponentId = 1,
                CheckUrl = "https://delete.com",
                CheckInterval = 30,
                CheckTimeout = 5,
                RequestHeaders = "X-Test: delete",
                DownAlertDelay = 1,
                DownAlertResend = 2,
                DownAlertMessage = "To be deleted",
                AlertEmail = "delete@example.com"
            };
            context.UptimeChecks.Add(entity);
            await context.SaveChangesAsync();

            var service = new UptimeCheckService(context);
            var result = await service.DeleteAsync(entity.Id);

            Assert.True(result);
            var deleted = await context.UptimeChecks.FindAsync(entity.Id);
            Assert.Null(deleted);
        }
    }
}
