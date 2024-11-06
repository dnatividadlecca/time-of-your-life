using Microsoft.EntityFrameworkCore;
using time.Controllers;
using time_of_your_life.Controllers;

namespace time_of_your_life.DbAccess
{
    public class DataBaseContext : DbContext
    {
        public DbSet<ClockProps> clockProps { get; set; } // Table for presets
        public DbSet<AlarmProps> alarmProps { get; set; } // Table for alarms

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=time_of_your_life.db");
            }
        }
    }
}
