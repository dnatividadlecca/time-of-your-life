using Microsoft.EntityFrameworkCore;
using time.Controllers;
using time_of_your_life.Controllers;

namespace time_of_your_life.Interfaces
{
    public interface IClockPropsContext
    {
        #region presets
        Task<IEnumerable<ClockProps>> ListAllPresets();

        Task<ClockProps> GetPresetById(int id);

        Task<ClockProps> SavePreset(ClockProps preset);

        Task<ClockProps> UpdatePreset(ClockProps updatedPreset);
        #endregion

        #region time zones
        Task<IEnumerable<ClockTimeZone>> ListAllTimeZones();

        Task<ClockTimeZone> SaveTimeZone(ClockTimeZone newTimeZone);
        #endregion

        #region alarms
        Task<IEnumerable<AlarmProps>> ListAllAlarms();

        Task<AlarmProps> GetAlarmById(int id);

        Task<AlarmProps> SaveAlarm(AlarmProps newAlarm);

        Task<AlarmProps> UpdateAlarm(AlarmProps updatedAlarm);
        #endregion
    }
}
