using Microsoft.AspNetCore.Mvc;
using time_of_your_life.Controllers;
using time_of_your_life.Interfaces;

namespace time.Controllers;

[ApiController]
[Route("[controller]")]
public class ClockController : ControllerBase
{
    private readonly ILogger<ClockController> _logger;
    private readonly IClockPropsContext _context;

    public ClockController(ILogger<ClockController> logger, IClockPropsContext clockPropsContext)
    {
        _logger = logger;
        _context = clockPropsContext;
    }

    [HttpGet, Route("presets")]
    public async Task<ActionResult<IEnumerable<ClockProps>>> GetPresets()
    {
        List<ClockProps> presets;
        try
        {
            _logger.LogInformation("Trying to get all presets");
            presets = (List<ClockProps>)await _context.ListAllPresets();
            _logger.LogInformation("Presets retrieved from database");
        }
        catch (Exception)
        {
            _logger.LogError("An error occurred while fetching all presets. Empty list will be loaded");
            presets = new List<ClockProps>();
        }

        return Ok(presets);
    }

    [HttpGet, Route("presets/{id}")]
    public async Task<ActionResult<ClockProps>> GetPresetById(int id)
    {
        try
        {
            _logger.LogInformation("Trying to get preset by {id} ", id);
            var preset = await _context.GetPresetById(id);

            if (preset == null)
            {
                _logger.LogWarning("Preset with ID {id} not found.", id);
                return NotFound("Preset not found.");
            }

            _logger.LogInformation("Preset retrieved from database");
            return Ok(preset);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching preset with ID {id} ", id);
            return StatusCode(500, new { message = ex.Message });
        }

    }

    [HttpPost, Route("presets")]
    public async Task<ActionResult> SavePreset([FromBody] ClockProps preset)
    {
        try
        {
            _logger.LogInformation("Validating model of new preset...");
            if (preset == null)
            {
                _logger.LogError("An error occurred while saving the preset. Preset is null");
                return BadRequest("Preset data is required.");
            }
            else if (!ModelState.IsValid)
            {
                _logger.LogError("An error occurred while saving the preset. Preset is not valid");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Trying to save new preset...");
            var savedPreset = await _context.SavePreset(preset);

            if(savedPreset.ID != 0)
            {
                _logger.LogInformation("Preset saved successfully.");
                return Ok("Preset saved successfully.");
            }
            else
            {
                _logger.LogError("An error occurred while saving the preset.");
                return BadRequest("An error occurred while saving the preset.");
            }
        }catch (Exception ex) {
            _logger.LogError("An error occurred while saving the preset. " + ex.Message);
            return BadRequest("An error occurred while saving the preset. " + ex.Message);
        }

    }

    [HttpPut, Route("presets")]
    public async Task<IActionResult> UpdatePreset([FromBody] ClockProps updatedPreset)
    {
        try
        {
            _logger.LogInformation("Validating model of new preset...");
            if (updatedPreset == null || updatedPreset.ID == 0)
            {
                _logger.LogWarning("Cannot update preset because no ID was assigned");
                return BadRequest("Cannot update preset because no ID was assigned");
            }
            else if (!ModelState.IsValid)
            {
                _logger.LogError("An error occurred while updating the preset. Preset is not valid");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Updating preset with ID: {Id}", updatedPreset.ID);
            await _context.UpdatePreset(updatedPreset);

            _logger.LogInformation("Preset updated successfully.");
            return Ok("Preset updated successfully.");

        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating the preset. " + ex.Message);
            return BadRequest($"An error occurred while updating the preset. {ex.Message}");
        }
    }

    [HttpGet, Route("serverTime")]
    public IActionResult GetServerTime()
    {
        try
        {
            _logger.LogInformation("Fetching server time...");
            var currentTime = DateTime.UtcNow;
            return Ok(new { currentTime = currentTime.ToString("o") });
        }
        catch (Exception ex) {
            _logger.LogError($"An error occurred while fetching server time. {ex.Message}");
            return BadRequest($"An error occurred while fetching server time. {ex.Message}");
        }
        
    }

    [HttpGet, Route("timezones")]
    public async Task<ActionResult<IEnumerable<ClockTimeZone>>> GetTimeZones()
    {
        List<ClockTimeZone> _timeZones;
        try
        {
            _logger.LogInformation("Trying to get all time zones");
            _timeZones = (List<ClockTimeZone>)await _context.ListAllTimeZones();
            _logger.LogInformation("Time zones retrieved from database");
        }
        catch (Exception)
        {
            _logger.LogError("An error occurred while fetching all time zones. Empty list will be loaded");
            _timeZones = new List<ClockTimeZone>();
        }

        return Ok(_timeZones);
    }

    [HttpPost, Route("timezones")]
    public async Task<IActionResult> SaveTimeZone([FromBody] ClockTimeZone newTimeZone)
    {
        try
        {
            _logger.LogInformation("Validating model of new time zone...");
            if (newTimeZone == null)
            {
                _logger.LogError("An error occurred while saving the time zone. Object is null");
                return BadRequest("Time zone data is required.");
            }
            else if (!ModelState.IsValid)
            {
                _logger.LogError("An error occurred while saving the time zone. Object is not valid");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Trying to save new time zone...");
            var savedPreset = await _context.SaveTimeZone(newTimeZone);

            if (savedPreset.ID != 0)
            {
                _logger.LogInformation("Time zone saved successfully.");
                return Ok("Time zone saved successfully.");
            }
            else
            {
                _logger.LogError("An error occurred while saving the time zone.");
                return BadRequest("An error occurred while saving the time zone.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while saving the time zone. " + ex.Message);
            return BadRequest("An error occurred while saving the time zone. " + ex.Message);
        }

    }

    [HttpPost, Route("alarms")]
    public async Task<ActionResult> SaveAlarm([FromBody] AlarmProps newAlarm)
    {
        try
        {
            _logger.LogInformation("Validating model of new alarm...");
            if (newAlarm == null)
            {
                _logger.LogError("An error occurred while saving the alarm. Preset is null");
                return BadRequest("Preset data is required.");
            }
            else if (!ModelState.IsValid)
            {
                _logger.LogError("An error occurred while saving the alarm. Preset is not valid");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Trying to save new alarm...");
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

    [HttpPut, Route("alarms")]
    public async Task<IActionResult> UpdateAlarm([FromBody] AlarmProps updatedAlarm)
    {
        try
        {
            _logger.LogInformation("Validating model of new alarm...");
            if (updatedAlarm == null || updatedAlarm.Id == 0)
            {
                _logger.LogWarning("Cannot update alarm because no ID was assigned");
                return BadRequest("Cannot update alarm because no ID was assigned");
            }
            else if (!ModelState.IsValid)
            {
                _logger.LogError("An error occurred while updating the alarm. Preset is not valid");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Updating alarm with ID: {Id}", updatedAlarm.Id);
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
