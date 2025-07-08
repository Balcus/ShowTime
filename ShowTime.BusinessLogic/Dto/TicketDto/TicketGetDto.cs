using ShowTime.DataAccess.Enums;

namespace ShowTime.BusinessLogic.Dto.TicketDto;

public class TicketGetDto
{
    public int Id { get; set; }
    public int FestivalId { get; set; }
    public string Name { get; set; } = null!;
    public TicketType Type { get; set; } = TicketType.Regular;
    public float Price { get; set; }
    public int Quantity { get; set; }
}