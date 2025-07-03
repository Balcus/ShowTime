using ShowTime.BusinessLogic.Dto.ArtistDto;
using ShowTime.BusinessLogic.Dto.LineupDto;

namespace ShowTime.BusinessLogic.Dto.FestivalDto;

public class FullFestivalDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string SplashArt { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public IList<LineupGetDto> Lineups { get; set; } = new List<LineupGetDto>();
    public IList<ArtistGetDto> Artists { get; set; } = new List<ArtistGetDto>();
}