using time.Controllers;
using time_of_your_life.Controllers;

namespace time_of_your_life.Interfaces
{
    public interface IAlarmPropsContext
    {
        Task<AlarmProps> SaveAlarm(AlarmProps newAlarm);

        Task<IEnumerable<AlarmProps>> ListAllAlarms();

        Task<AlarmProps> GetAlarmById(int id);

        Task<AlarmProps> UpdateAlarm(AlarmProps updatedAlarm);
    }
}
