using System.ComponentModel.DataAnnotations;

namespace time_of_your_life.Controllers
{
    public class AlarmProps
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Execution Hour is required.")]
        public string ExecutionHour { get; set; }
        [Required(ErrorMessage = "Status is required.")]
        public bool Status { get; set; }
    }
}
