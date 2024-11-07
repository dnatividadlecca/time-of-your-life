using System.ComponentModel.DataAnnotations;
using System.Data;

namespace time.Controllers;

public class ClockProps {
    public int ID { get; set; }

    [Required(ErrorMessage = "Font Family is required.")]
    public string FontFamily {get; set;}
    
    [Range(8, 100, ErrorMessage = "Title Font size must be between 1 and 100.")]
    public int TitleFontSize {get; set;}
    
    [Range(8, 100, ErrorMessage = "Clock Font size must be between 1 and 100.")]
    public int ClockFontSize {get ; set;}
    
    public bool BlinkColons {get; set;}
    
    [Required(ErrorMessage = "Title color is required.")]
    public string TitleFontColor { get; set; }
    
    [Required(ErrorMessage = "Clock color is required.")]
    public string ClockFontColor { get; set; }

}