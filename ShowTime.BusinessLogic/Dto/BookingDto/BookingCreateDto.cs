using System.ComponentModel.DataAnnotations;

namespace ShowTime.BusinessLogic.Dto.BookingDto;

public class BookingCreateDto
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public int TicketId { get; set; }
}