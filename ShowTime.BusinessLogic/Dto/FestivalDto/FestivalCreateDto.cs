using System.ComponentModel.DataAnnotations;

namespace ShowTime.BusinessLogic.Dto.FestivalDto;

public class FestivalCreateDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "Location is required")]
    public string Location { get; set; } = string.Empty;
    [Required(ErrorMessage = "Start Date is required")]
    public DateTime StartDate { get; set; } = DateTime.Now;
    [Required(ErrorMessage = "End Date is required")]
    public DateTime EndDate { get; set; } = DateTime.Now;
    [Required(ErrorMessage = "Splash art is required")]
    public string SplashArt { get; set; } = string.Empty;
}