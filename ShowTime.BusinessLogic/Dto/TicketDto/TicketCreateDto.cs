using System.ComponentModel.DataAnnotations;
using ShowTime.DataAccess.Enums;

namespace ShowTime.BusinessLogic.Dto.TicketDto;

public class TicketCreateDto
{
    [Required]
    public int FestivalId { get; set; }
    [Required]
    public String Name { get; set; } = null!;
    [Required]
    public TicketType Type { get; set; } = TicketType.Regular;
    [Required]
    public float Price { get; set; }
    [Required]
    public int Quantity { get; set; }
}