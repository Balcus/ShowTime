using ShowTime.BusinessLogic.Dto.BookingDto;
using ShowTime.DataAccess.Enums;

namespace ShowTime.BusinessLogic.Dto.UserDto;

public class UserGetDto
{
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.User;
    public List<BookingGetDto> Bookings { get; set; } = [];
}