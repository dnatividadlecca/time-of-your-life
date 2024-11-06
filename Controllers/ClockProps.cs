using System.ComponentModel.DataAnnotations;
using System.Data;

namespace time.Controllers;

public class ClockProps {
    public int ID { get; set; }
    public string FontFamily {get; set;}
    //public int[] AvailableFontSizes {get; }  = new[] { 12, 24, 48, 64 };
    [Range(8, 100, ErrorMessage = "Title Font size must be between 1 and 100.")]
    public int TitleFontSize {get; set;}
    [Range(8, 100, ErrorMessage = "Clock Font size must be between 1 and 100.")]
    public int ClockFontSize {get ; set;}
    public bool BlinkColons {get; set;}
    //public string FontColor {get; set;}
    [Required(ErrorMessage = "Title color is required.")]
    public string TitleFontColor { get; set; }
    [Required(ErrorMessage = "Clock color is required.")]
    public string ClockFontColor { get; set; }

    //public ClockProps()
    //{
    //    //ID = 0;
    //    //FontFamily = "courier";
    //    //TitleFontSize = 64;
    //    //ClockFontSize = 48;
    //    //BlinkColons = true;
    //    ////FontColor = "black";
    //    //TitleFontColor = "black";
    //    //ClockFontColor = "black";
    //}
}