using ShowTime.DataAccess.Enums;

namespace ShowTime.BusinessLogic.Dto.BookingDto;

public class BookingGetDto
{
    public int FestivalId { get; set; }
    public int UserId { get; set; }
    public BookingType Type { get; set; } = BookingType.Regular;
    public float Price { get; set; }
}