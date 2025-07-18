using ShowTime.BusinessLogic.Dto.ArtistDto;
using ShowTime.BusinessLogic.Dto.LineupDto;
using ShowTime.BusinessLogic.Dto.TicketDto;

namespace ShowTime.BusinessLogic.Dto.FestivalDto;

public class FestivalGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string SplashArt { get; set; } = string.Empty;
    public int TotalAvailableTickets => Tickets.Sum(t => t.Quantity);
    public ICollection<TicketGetDto> Tickets { get; set; } = new List<TicketGetDto>();
}