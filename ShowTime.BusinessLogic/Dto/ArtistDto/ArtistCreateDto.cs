using System.ComponentModel.DataAnnotations;

namespace ShowTime.BusinessLogic.Dto.ArtistDto;

public class ArtistCreateDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "Genre is required")]
    public string? Genre { get; set; } = string.Empty;
    [Required(ErrorMessage = "Image is required")]
    public string? Image { get; set; } = string.Empty;
}