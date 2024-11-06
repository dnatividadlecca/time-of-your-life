using Microsoft.EntityFrameworkCore;
using time.Controllers;
using time_of_your_life.Controllers;
using time_of_your_life.Interfaces;

namespace time_of_your_life.DbAccess
{
    public class AlarmPropsContext : IAlarmPropsContext
    {
        private readonly DataBaseContext _context;
        
        public AlarmPropsContext(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<AlarmProps> GetAlarmById(int id)
        {
            try
            {
                return await _context.alarmProps.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("There was an issue trying to get alarm id#" + id, ex);
            }
        }

        public async Task<IEnumerable<AlarmProps>> ListAllAlarms()
        {
            try
            {
                return await _context.alarmProps.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("The was an issue trying to list all alarms", ex);
            }
        }

        public async Task<AlarmProps> SaveAlarm(AlarmProps newAlarm)
        {
            try
            {
                _context.alarmProps.Add(newAlarm);
                await _context.SaveChangesAsync();
                return newAlarm;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("The alarm could not be saved", ex);
            }
        }

        public async Task<AlarmProps> UpdateAlarm(AlarmProps updatedAlarm)
        {
            try
            {
                var existingAlarm = await _context.alarmProps.FindAsync(updatedAlarm.Id);

                if (existingAlarm == null)
                {
                    throw new Exception("There was an issue trying to get alarm id#" + updatedAlarm.Id);
                }

                existingAlarm.ExecutionHour = updatedAlarm.ExecutionHour;
                existingAlarm.Status = updatedAlarm.Status;

                _context.alarmProps.Update(existingAlarm);
                await _context.SaveChangesAsync();

                return existingAlarm;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("The alarm could not be updated", ex);
            }
        }
    }
}
