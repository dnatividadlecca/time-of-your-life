using System.ComponentModel.DataAnnotations;

namespace time_of_your_life.Controllers
{
    public class ClockTimeZone
    {
        public int ID { get; set; }
        
        [Required(ErrorMessage = "Time Zone is required.")]
        public string Zone { get; set; }
        
    }
}
