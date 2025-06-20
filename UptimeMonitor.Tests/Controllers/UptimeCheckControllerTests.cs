using Microsoft.AspNetCore.Mvc;
using Moq;
using UptimeMonitor.API.Controllers;
using UptimeMonitor.API.DTOs;
using UptimeMonitor.API.Interfaces;
using Xunit;

namespace UptimeMonitor.Tests.Controllers
{
    public class UptimeCheckControllerTests
    {
        private readonly Mock<IUptimeCheckService> _mockService;
        private readonly UptimeCheckController _controller;

        public UptimeCheckControllerTests()
        {
            _mockService = new Mock<IUptimeCheckService>();
            _controller = new UptimeCheckController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkWithItems()
        {
            var mockList = new List<UptimeCheckResponseDto>
            {
                new UptimeCheckResponseDto { Id = 1, Name = "Check A" },
                new UptimeCheckResponseDto { Id = 2, Name = "Check B" }
            };

            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(mockList);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var data = Assert.IsAssignableFrom<IEnumerable<UptimeCheckResponseDto>>(okResult.Value);
            Assert.Equal(2, data.Count());
        }

        [Fact]
        public async Task GetById_WhenFound_ReturnsOk()
        {
            var dto = new UptimeCheckResponseDto { Id = 1, Name = "Check A" };
            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(dto);

            var result = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var data = Assert.IsType<UptimeCheckResponseDto>(okResult.Value);
            Assert.Equal(1, data.Id);
        }

        [Fact]
        public async Task GetById_WhenNotFound_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetByIdAsync(999)).ReturnsAsync((UptimeCheckResponseDto?)null);

            var result = await _controller.GetById(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction()
        {
            var createDto = new UptimeCheckCreateDto { Name = "New Check" };
            var createdDto = new UptimeCheckResponseDto { Id = 3, Name = "New Check" };

            _mockService.Setup(s => s.CreateAsync(createDto)).ReturnsAsync(createdDto);

            var result = await _controller.Create(createDto);

            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var data = Assert.IsType<UptimeCheckResponseDto>(created.Value);
            Assert.Equal(3, data.Id);
        }

        [Fact]
        public async Task Update_WhenSuccess_ReturnsNoContent()
        {
            _mockService.Setup(s => s.UpdateAsync(1, It.IsAny<UptimeCheckCreateDto>())).ReturnsAsync(true);

            var result = await _controller.Update(1, new UptimeCheckCreateDto());

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_WhenNotFound_ReturnsNotFound()
        {
            _mockService.Setup(s => s.UpdateAsync(999, It.IsAny<UptimeCheckCreateDto>())).ReturnsAsync(false);

            var result = await _controller.Update(999, new UptimeCheckCreateDto());

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
