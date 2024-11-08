using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.ComponentModel.DataAnnotations;
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

        [Fact]
        public async Task CheckIfPresetSaves()
        {
            var newPreset = new ClockProps
            {
                FontFamily = "Times",
                TitleFontSize = 30,
                ClockFontSize = 88,
                TitleFontColor = "#c91d1d",
                ClockFontColor = "#4fce2c"
            };
            _mockClockService.Setup(service => service.SavePreset(newPreset)).ReturnsAsync(newPreset);

            var result = await _controller.SavePreset(newPreset);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnResult = Assert.IsType<ClockProps>(createdResult.Value);

            Assert.Equal("Times", returnResult.FontFamily);
            Assert.Equal(88, returnResult.TitleFontSize);
        }

        [Fact]
        public async Task CheckIfUpdatesSaves()
        {
            var updatedPreset = new ClockProps
            {
                ID = 1,
                FontFamily = "Times",
                TitleFontSize = 30,
                ClockFontSize = 88,
                TitleFontColor = "#c91d1d",
                ClockFontColor = "#4fce2c"
            };
            _mockClockService.Setup(service => service.UpdatePreset(updatedPreset)).ReturnsAsync(updatedPreset);

            var result = await _controller.UpdatePreset(updatedPreset);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
