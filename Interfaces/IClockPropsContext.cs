using Microsoft.EntityFrameworkCore;
using time.Controllers;
using time_of_your_life.Controllers;

namespace time_of_your_life.Interfaces
{
    public interface IClockPropsContext
    {
        Task<ClockProps> SavePreset(ClockProps preset);

        Task<IEnumerable<ClockProps>> ListAllPresets();

        Task<ClockProps> GetPresetById(int id);

        Task<ClockProps> UpdatePreset(ClockProps updatedPreset);

        Task<IEnumerable<ClockTimeZone>> ListAllTimeZones();

        Task<ClockTimeZone> SaveTimeZone(ClockTimeZone newTimeZone);
    }
}
