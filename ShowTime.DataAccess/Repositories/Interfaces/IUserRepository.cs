using ShowTime.DataAccess.Enums;
using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User> LoginAsync(string email, string password);
    Task RegisterUserAsync(User user);
    Task<List<User>> GetAllUsersAsync();
    Task<int> GetUserIdByEmailAsync(string? email);
    Task BookTicketAsync(int userId, int ticketId);
    Task<List<Booking>> GetUserBookingsAsync(int userId);
    Task<List<Ticket>> GetUserTicketsAsync(int userId);
    Task DeleteUserBookingAsync(int userId, int ticketId);
}