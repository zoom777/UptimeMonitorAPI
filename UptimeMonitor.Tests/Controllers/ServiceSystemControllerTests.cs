using Microsoft.AspNetCore.Mvc;
using Moq;
using UptimeMonitor.API.Controllers;
using UptimeMonitor.API.DTOs;
using UptimeMonitor.API.Interfaces;
using Xunit;

namespace UptimeMonitor.Tests.Controllers
{
    public class ServiceSystemControllerTests
    {
        private readonly Mock<IServiceSystemService> _mockService;
        private readonly ServiceSystemController _controller;

        public ServiceSystemControllerTests()
        {
            _mockService = new Mock<IServiceSystemService>();
            _controller = new ServiceSystemController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkWithList()
        {
            // Arrange
            var mockList = new List<ServiceSystemResponseDto>
            {
                new ServiceSystemResponseDto { Id = 1, Name = "System A", Description = "Desc A", Status = true },
                new ServiceSystemResponseDto { Id = 2, Name = "System B", Description = "Desc B", Status = true }
            };
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(mockList);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<ServiceSystemResponseDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        [Fact]
        public async Task GetById_WhenFound_ReturnsOk()
        {
            var dto = new ServiceSystemResponseDto { Id = 1, Name = "System", Description = "Test", Status = true };
            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(dto);

            var result = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ServiceSystemResponseDto>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetById_WhenNotFound_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetByIdAsync(999)).ReturnsAsync((ServiceSystemResponseDto?)null);

            var result = await _controller.GetById(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction()
        {
            var dto = new ServiceSystemCreateDto { Name = "New", Description = "New Desc" };
            var created = new ServiceSystemResponseDto { Id = 5, Name = "New", Description = "New Desc", Status = true };

            _mockService.Setup(s => s.CreateAsync(dto)).ReturnsAsync(created);

            var result = await _controller.Create(dto);

            var createdAt = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<ServiceSystemResponseDto>(createdAt.Value);
            Assert.Equal(5, returnValue.Id);
        }

        [Fact]
        public async Task Update_WhenSuccess_ReturnsNoContent()
        {
            _mockService.Setup(s => s.UpdateAsync(1, It.IsAny<ServiceSystemCreateDto>())).ReturnsAsync(true);

            var result = await _controller.Update(1, new ServiceSystemCreateDto());

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_WhenNotFound_ReturnsNotFound()
        {
            _mockService.Setup(s => s.UpdateAsync(999, It.IsAny<ServiceSystemCreateDto>())).ReturnsAsync(false);

            var result = await _controller.Update(999, new ServiceSystemCreateDto());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_WhenSuccess_ReturnsNoContent()
        {
            _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

            var result = await _controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_WhenNotFound_ReturnsNotFound()
        {
            _mockService.Setup(s => s.DeleteAsync(999)).ReturnsAsync(false);

            var result = await _controller.Delete(999);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
