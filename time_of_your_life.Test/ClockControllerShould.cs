using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using time.Controllers;
using time_of_your_life.Interfaces;

namespace time_of_your_life.Test
{
    public class ClockControllerShould
    {
        private readonly Mock<IClockPropsContext> _mockClockService;

        private readonly Mock<ILogger<ClockController>> _mockLogger;
        private readonly ClockController _controller;

        public ClockControllerShould()
        {
            _mockClockService = new Mock<IClockPropsContext>();
            _mockLogger = new Mock<ILogger<ClockController>>();
            _controller = new ClockController(_mockLogger.Object, _mockClockService.Object);
        }

        [Fact]
        public async Task CheckIfNoPresetsAreSaved()
        {
            var presets = new List<ClockProps>();
            _mockClockService.Setup(service => service.ListAllPresets()).ReturnsAsync(presets);

            var result = await _controller.GetPresets();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<ClockProps>>(okResult.Value);
            Assert.Empty(returnValue);
        }

        [Fact]
        public async Task CheckIfPresetDoesNotExists()
        {
            var presetId = 99;
            _mockClockService.Setup(service => service.GetPresetById(presetId)).ReturnsAsync((ClockProps)null);

            // Act
            var result = await _controller.GetPresetById(presetId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}
