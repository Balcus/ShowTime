using ShowTime.BusinessLogic.Dto.BookingDto;
using ShowTime.BusinessLogic.Dto.TicketDto;
using ShowTime.BusinessLogic.Dto.UserDto;
using ShowTime.DataAccess.Models;

namespace ShowTime.BusinessLogic.Abstractions;

public interface IUserService
{
    Task RegisterUserAsync(LoginDto userCreateDto);
    Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
    Task<List<UserGetDto>> GetAllUsersAsync();
    Task<int> GetUserIdByEmailAsync(string? email);
    Task BookTicketAsync(int userId, int ticketId);
    Task<List<BookingGetDto>> GetUserBookings(int userId);
    Task<List<TicketGetDto>> GetUserTickets(int userId);
    Task DeleteUserBookingAsync(int userId, int ticketId);
}