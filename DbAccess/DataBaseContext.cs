using Microsoft.EntityFrameworkCore;
using time.Controllers;

namespace time_of_your_life.DbAccess
{
    public class DataBaseContext : DbContext
    {
        public DbSet<ClockProps> clockProps { get; set; } // Table for presets

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
