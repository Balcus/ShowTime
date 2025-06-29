using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Interfaces;

namespace ShowTime.DataAccess.Repositories;

public class UserRepository(DbContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<ICollection<Booking>?> GetUserBookingsAsync(int id)
    {
        try
        {
            var user = await _context
                .Set<User>()
                .Include(u => u.Bookings)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user?.Bookings;
        }
        catch (Exception e)
        {
            throw new Exception($"Error trying to retrieve bookings for user with ID {id}: {e.Message}");
        }
    }
}