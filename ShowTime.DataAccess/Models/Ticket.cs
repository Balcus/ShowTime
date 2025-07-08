using ShowTime.DataAccess.Enums;

namespace ShowTime.DataAccess.Models;

public class Ticket
{
    public int Id { get; set; }
    public int FestivalId { get; set; }
    public string Name { get; set; } = null!;
    public TicketType Type { get; set; } = TicketType.Regular;
    public float Price { get; set; }
    public int Quantity { get; set; }
    
    public Festival Festival { get; set; } = null!;
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}