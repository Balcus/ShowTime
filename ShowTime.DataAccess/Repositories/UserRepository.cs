using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Interfaces;
using ShowTime.DataAccess.Security;

namespace ShowTime.DataAccess.Repositories;

public class UserRepository(ShowTimeDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<ICollection<Booking>?> GetUserBookingsAsync(int id)
    {
        try
        {
            var user = await Context
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

    public async Task<User> LoginAsync(string email, string providedPassword)
    {
        var user = await Context
            .Set<User>()
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
        {
            throw new Exception($"User with email {email} does not exist");
        }
        
        var isSame = PasswordHasher.ComparePasswords(user.Password, providedPassword);
        if (isSame)
        {
            return user;
        }

        throw new Exception($"The provided password is not correct");
    }

    public async Task RegisterUserAsync(User user)
    {
        try
        {
            var passwordHash = PasswordHasher.HashPassword(user.Password);
            user.Password = passwordHash;
            await Context
                .Set<User>()
                .AddAsync(user);
            await Context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Error trying to register user with email {user.Email}: {e.Message}");
        }
    }
}