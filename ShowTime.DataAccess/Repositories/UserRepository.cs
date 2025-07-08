using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.Exceptions;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Interfaces;
using ShowTime.DataAccess.Security;

namespace ShowTime.DataAccess.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly ShowTimeDbContext _context;
    private readonly IRepository<Ticket> _ticketRepository;
    public UserRepository(ShowTimeDbContext context, IRepository<Ticket> ticketRepository)
        : base(context)
    {
        _context = context;
        _ticketRepository = ticketRepository;
    }
    public async Task<List<Booking>> GetUserBookingsAsync(int userId)
    {
        try
        {
            var user = await Context
                .Set<User>()
                .Include(u => u.Bookings)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user != null ? user.Bookings.ToList() : [];
        }
        catch (Exception e)
        {
            throw new Exception($"Error trying to retrieve bookings for user with ID {userId}: {e.Message}");
        }
    }

    public async Task<List<Ticket>> GetUserTicketsAsync(int userId)
    {
        try
        {
            var bookings = await Context
                .Set<Booking>()
                .Include(b => b.Ticket)
                .Where(b => b.UserId == userId)
                .ToListAsync();

            return bookings.Count > 0 ? bookings.Select(b => b.Ticket).ToList() : [];
        }
        catch (Exception e)
        {
            throw new Exception($"Error trying to retrieve tickets for user with ID {userId}: {e.Message}");
        }
    }

    public async Task DeleteUserBookingAsync(int userId, int ticketId)
    {
        try
        {
            var bookings = await Context
                .Set<Booking>()
                .Include(b => b.Ticket)
                .Where(b => b.UserId == userId && b.TicketId == ticketId)
                .FirstOrDefaultAsync();

            if (bookings != null)
            {
                bookings.Ticket.Quantity += 1;
                Context.Set<Booking>().Remove(bookings);
                await Context.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            throw new Exception($"Error trying to delete user booking with ID {userId}: {e.Message}");
        }
    }


    public async Task<User> LoginAsync(string email, string providedPassword)
    {
        try
        {
            var user = await Context
                .Set<User>()
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                throw new UserDoesntExistException();
            }

            var isSame = PasswordHasher.ComparePasswords(user.Password, providedPassword);
            if (isSame)
            {
                return user;
            }

            throw new WrongPasswordError();
        }
        catch (WrongPasswordError e)
        {
            throw;
        }
        catch (UserDoesntExistException e)
        {
            throw;
        }
        catch (Exception e)
        {
            throw new Exception($"Error trying to login user with email {email}: {e.Message}");
        }
    }

    public async Task RegisterUserAsync(User user)
    {
        try
        {
            var sameMailUser = await Context.Set<User>().Where(u => u.Email == user.Email).FirstOrDefaultAsync();
            if (sameMailUser != null)
            {
                throw new UserAlreadyExistsException(user.Email);
            }

            var passwordHash = PasswordHasher.HashPassword(user.Password);
            user.Password = passwordHash;
            await Context
                .Set<User>()
                .AddAsync(user);
            await Context.SaveChangesAsync();
        }
        catch (UserAlreadyExistsException e)
        {
            throw;
        }
        catch (Exception e)
        {
            throw new Exception($"Error trying to register user with email {user.Email}: {e.Message}");
        }
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        try
        {
            return await Context
                .Set<User>()
                .Include(u => u.Bookings)
                .ToListAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Error trying to get all users: {e.Message}");
        }
    }

    public async Task<int> GetUserIdByEmailAsync(string? email)
    {
        try
        {
            var user = await Context
                .Set<User>()
                .FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                throw new UserDoesntExistException();
            }

            return user.Id;
        }
        catch (UserDoesntExistException e)
        {
            throw;
        }
        catch (Exception e)
        {
            throw new Exception($"Error trying to get user with email {email}: {e.Message}");
        }
    }

    public async Task BookTicketAsync(int userId, int ticketId)
    {
        try
        {
            var user = await Context
                .Set<User>()
                .Include(u => u.Bookings)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new UserDoesntExistException();
            }

            var newBooking = new Booking()
            {
                UserId = userId,
                TicketId = ticketId,
            };

            var ticket = await _ticketRepository.GetByIdAsync(ticketId);
            if (ticket == null)
            {
                throw new EntityNotFoundException("Ticket type with id " + ticketId + " not found");
            }

            ticket.Quantity -= 1;
            user.Bookings.Add(newBooking);
            await Context.SaveChangesAsync();
        }
        catch (UserDoesntExistException e)
        {
            throw;
        }
        catch (EntityNotFoundException e)
        {
            throw;
        }
        catch (Exception e)
        {
            throw new Exception($"Error trying to book user with id {userId}: {e.Message}");
        }
    }
    
    
}