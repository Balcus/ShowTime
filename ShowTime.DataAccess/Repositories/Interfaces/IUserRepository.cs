using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<ICollection<Booking>?> GetUserBookingsAsync(int id);
}