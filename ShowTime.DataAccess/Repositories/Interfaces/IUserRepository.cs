using ShowTime.DataAccess.Enums;
using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<ICollection<Booking>?> GetUserBookingsAsync(int id);
    Task<User> LoginAsync(string email, string password);
    Task RegisterUserAsync(User user);
}