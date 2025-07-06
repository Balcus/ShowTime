using System.ComponentModel.DataAnnotations;

namespace ShowTime.BusinessLogic.Dto.LineupDto;

public class LineupGetDto
{
    [Required]
    public int FestivalId { get; set; }
    [Required(ErrorMessage = "Artist field is required")]
    public int ArtistId { get; set; }
    [Required(ErrorMessage = "Stage is required")]
    public string Stage { get; set; } = string.Empty;
    [Required(ErrorMessage = "Start Time is required")]
    public DateTime StartTime { get; set; }
}