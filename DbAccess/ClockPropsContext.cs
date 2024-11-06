using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using time.Controllers;
using time_of_your_life.Interfaces;

namespace time_of_your_life.DbAccess
{
    public class ClockPropsContext : IClockPropsContext
    {
        private readonly DataBaseContext _context;

        public ClockPropsContext(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<ClockProps> SavePreset(ClockProps preset)
        {
            try
            {
                _context.clockProps.Add(preset);
                await _context.SaveChangesAsync();
                return preset;

            }
            catch (Exception ex) {
                throw new InvalidOperationException("The preset could not be saved", ex);
            }            
        }

        public async Task<IEnumerable<ClockProps>> ListAllPresets()
        {
            try
            {
                return await _context.clockProps.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("The preset could not be saved", ex);
            }
        }

        public async Task<ClockProps> GetPresetById(int id)
        {
            try
            {
                return await _context.clockProps.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("There was an issue trying to get preset id#" + id, ex);
            }
        }

        public async Task<ClockProps> UpdatePreset(ClockProps updatedPreset)
        {
            try
            {
                var existingPreset = await _context.clockProps.FindAsync(updatedPreset.ID);

                if (existingPreset == null)
                {
                    throw new Exception("There was an issue trying to get preset id#" + updatedPreset.ID);
                }

                existingPreset.FontFamily = updatedPreset.FontFamily;
                existingPreset.TitleFontSize = updatedPreset.TitleFontSize;
                existingPreset.ClockFontSize = updatedPreset.ClockFontSize;
                existingPreset.BlinkColons = updatedPreset.BlinkColons;
                existingPreset.TitleFontColor = updatedPreset.TitleFontColor;
                existingPreset.ClockFontColor = updatedPreset.ClockFontColor;

                _context.clockProps.Update(existingPreset);
                await _context.SaveChangesAsync();

                return existingPreset;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("The preset could not be saved", ex);
            }
        }
    }
}
