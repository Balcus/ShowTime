namespace ShowTime.DataAccess.Models;

public class Festival
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string SplashArt { get; set; } = string.Empty;
    public int TotalAvailableTickets => Tickets.Sum(t => t.Quantity);
    
    public ICollection<Lineup> Lineups { get; set; } = new List<Lineup>();
    public ICollection<Artist> Artists { get; set; } = new List<Artist>();
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}