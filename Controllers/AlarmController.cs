using Microsoft.AspNetCore.Mvc;
using time.Controllers;
using time_of_your_life.Interfaces;

namespace time_of_your_life.Controllers;

[ApiController]
[Route("[controller]")]
public class AlarmController : ControllerBase
{
    private readonly ILogger<AlarmController> _logger;
    private readonly IAlarmPropsContext _context;

    public AlarmController(ILogger<AlarmController> logger, IAlarmPropsContext alarmPropsContext)
    {
        _logger = logger;
        _context = alarmPropsContext;
    }

    [HttpGet("alarms")]
    public async Task<ActionResult<IEnumerable<AlarmProps>>> GetAlarms()
    {
        List<AlarmProps> alarms;
        try
        {
            _logger.LogInformation("Trying to get all alarms");
            alarms = (List<AlarmProps>)await _context.ListAllAlarms();
            _logger.LogInformation("Alarms retrieved from database");
        }
        catch (Exception)
        {
            _logger.LogError("An error occurred while fetching all alarms. Empty list will be loaded");
            alarms = new List<AlarmProps>();
        }

        return Ok(alarms);
    }

    [HttpGet("alarms/{id}")]
    public async Task<ActionResult<AlarmProps>> GetPresetById(int id)
    {
        try
        {
            _logger.LogInformation("Trying to get alarm by {id} ", id);
            var alarm = await _context.GetAlarmById(id);

            if (alarm == null)
            {
                _logger.LogWarning("Alarm with ID {id} not found.", id);
                return NotFound("Alarm not found.");
            }

            _logger.LogInformation("Alarm retrieved from database");
            return Ok(alarm);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching alarm with ID {id} ", id);
            return StatusCode(500, new { message = ex.Message });
        }

    }

    [HttpPost("alarms")]
    public async Task<IActionResult> SavePreset([FromBody] AlarmProps newAlarm)
    {
        try
        {
            _logger.LogInformation("Validating model of new alarm...");
            if (newAlarm == null)
            {
                _logger.LogError("An error occurred while saving the alarm. Alarm is null");
                return BadRequest("Alarm data is required.");
            }
            else if (!ModelState.IsValid)
            {
                _logger.LogError("An error occurred while saving the preset. Alarm is not valid");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Trying to save new preset...");
            var savedAlarm = await _context.SaveAlarm(newAlarm);

            if (savedAlarm.Id != 0)
            {
                _logger.LogInformation("Alarm saved successfully.");
                return Ok("Alarm saved successfully.");
            }
            else
            {
                _logger.LogError("An error occurred while saving the alarm.");
                return BadRequest("An error occurred while saving the alarm.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while saving the alarm. " + ex.Message);
            return BadRequest("An error occurred while saving the alarm. " + ex.Message);
        }

    }

    [HttpPut("alarms")]
    public async Task<IActionResult> UpdatePreset([FromBody] AlarmProps updatedAlarm)
    {
        try
        {
            _logger.LogInformation("Validating model of new preset...");
            if (updatedAlarm == null || updatedAlarm.Id == 0)
            {
                _logger.LogWarning("Cannot update alarm because no ID was assigned");
                return BadRequest("Cannot update alarm because no ID was assigned");
            }
            else if (!ModelState.IsValid)
            {
                _logger.LogError("An error occurred while updating the alarm. Alarm is not valid");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Updating preset with ID: {Id}", updatedAlarm.Id);
            await _context.UpdateAlarm(updatedAlarm);

            _logger.LogInformation("Alarm updated successfully.");
            return Ok("Alarm updated successfully.");

        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating the alarm. " + ex.Message);
            return BadRequest($"An error occurred while updating the alarm. {ex.Message}");
        }
    }
}