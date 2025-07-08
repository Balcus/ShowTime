using ShowTime.DataAccess.Enums;

namespace ShowTime.BusinessLogic.Dto.BookingDto;

public class BookingGetDto
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public int UserId { get; set; }
}