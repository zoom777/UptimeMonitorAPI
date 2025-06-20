using Microsoft.AspNetCore.Mvc;
using Moq;
using UptimeMonitor.API.Controllers;
using UptimeMonitor.API.DTOs;
using UptimeMonitor.API.Interfaces;

namespace UptimeMonitor.Tests.Controllers
{
    public class ComponentControllerTests
    {
        private readonly Mock<IComponentService> _mockService;
        private readonly ComponentController _controller;

        public ComponentControllerTests()
        {
            _mockService = new Mock<IComponentService>();
            _controller = new ComponentController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkWithList()
        {
            // Arrange
            var mockList = new List<ComponentResponseDto>
            {
                new ComponentResponseDto { Id = 1, Name = "Component A", Description = "Desc A", Status = true, ServiceSystemId = 1 },
                new ComponentResponseDto { Id = 2, Name = "Component B", Description = "Desc B", Status = true, ServiceSystemId = 1 }
            };
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(mockList);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<ComponentResponseDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        [Fact]
        public async Task GetById_WhenFound_ReturnsOk()
        {
            // Arrange
            var dto = new ComponentResponseDto { Id = 1, Name = "Component", Description = "Test", Status = true, ServiceSystemId = 1 };
            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(dto);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ComponentResponseDto>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetById_WhenNotFound_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetByIdAsync(999)).ReturnsAsync((ComponentResponseDto?)null);

            var result = await _controller.GetById(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction()
        {
            // Arrange
            var dto = new ComponentCreateDto { Name = "New", Description = "New Desc", ServiceSystemId = 1 };
            var createdDto = new ComponentResponseDto { Id = 5, Name = "New", Description = "New Desc", Status = true, ServiceSystemId = 1 };

            _mockService.Setup(s => s.CreateAsync(dto)).ReturnsAsync(createdDto);

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var createdAt = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<ComponentResponseDto>(createdAt.Value);
            Assert.Equal(5, returnValue.Id);
        }

        [Fact]
        public async Task Update_WhenSuccess_ReturnsOkWithBody()
        {
            var updatedDto = new ComponentResponseDto
            {
                Id = 1,
                Name = "Updated",
                Description = "Updated Desc",
                Status = true,
                ServiceSystemId = 1
            };

            _mockService
                .Setup(s => s.UpdateAsync(1, It.IsAny<ComponentCreateDto>()))
                .ReturnsAsync(updatedDto);

            var result = await _controller.Update(1, new ComponentCreateDto());

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ComponentResponseDto>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
            Assert.Equal("Updated", returnValue.Name);
        }

        [Fact]
        public async Task Update_WhenNotFound_ReturnsNotFound()
        {
            _mockService
                .Setup(s => s.UpdateAsync(999, It.IsAny<ComponentCreateDto>()))
                .ReturnsAsync((ComponentResponseDto?)null);

            var result = await _controller.Update(999, new ComponentCreateDto());

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
