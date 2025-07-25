namespace ShowTime.BusinessLogic.Dto.ArtistDto;
public class ArtistGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Genre { get; set; } = string.Empty;
    public string? Image { get; set; } = string.Empty;
}