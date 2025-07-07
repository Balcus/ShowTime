using ShowTime.BusinessLogic.Dto.BookingDto;
using ShowTime.BusinessLogic.Dto.UserDto;
using ShowTime.DataAccess.Models;

namespace ShowTime.BusinessLogic.Abstractions;

public interface IUserService
{
    Task RegisterUserAsync(LoginDto userCreateDto);
    Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
    Task<List<UserGetDto>> GetAllUsersAsync();
    Task BookTicketAsync(int userId, BookingGetDto booking);
    Task<int> GetUserIdByEmailAsync(string? email);
}