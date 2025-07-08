using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.Dto.BookingDto;
using ShowTime.BusinessLogic.Dto.TicketDto;
using ShowTime.BusinessLogic.Dto.UserDto;
using ShowTime.DataAccess.Exceptions;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Interfaces;

namespace ShowTime.BusinessLogic.Services;
public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task RegisterUserAsync(LoginDto userCreateDto)
    {
        try
        {
            var createUser = new User()
            {
                Email = userCreateDto.Email,
                Password = userCreateDto.Password,
            };
            await userRepository.RegisterUserAsync(createUser);
        }
        catch (UserAlreadyExistsException e)
        {
            throw;
        }
        catch (Exception e)
        {
            throw new Exception($"Error occured while trying to register user: {e.Message}");
        }
    }

    public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
    {
        try
        {
            var user = await userRepository.LoginAsync(loginDto.Email, loginDto.Password);
            return new LoginResponseDto()
            {
                Role = user.Role,
            };
        }
        catch (WrongPasswordError)
        {
            throw;
        }
        catch (UserDoesntExistException e)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo
            throw new Exception($"Error while trying to login: {e.Message}");
        }
    }

    public async Task<List<UserGetDto>> GetAllUsersAsync()
    {
        try
        {
            var users = await userRepository.GetAllUsersAsync();

            var result = users.Select(user => new UserGetDto
            {
                Email = user.Email,
                Role = user.Role,
                Bookings = user.Bookings.Select(b => new BookingGetDto
                {
                    TicketId = b.TicketId,
                    Id = b.Id,
                    UserId = b.UserId
                }).ToList()
            }).ToList();

            return result;
        }
        catch (Exception e)
        {
            throw new Exception($"Error occurred while trying to get all users: {e.Message}");
        }
    }

    public async Task<int> GetUserIdByEmailAsync(string? email)
    {
        try
        {
            return await userRepository.GetUserIdByEmailAsync(email);
        }
        catch (UserDoesntExistException)
        {
            throw new UserDoesntExistException();
        }
        catch (Exception e)
        {
            throw new Exception($"Error occured while trying to get userId with email {email}: {e.Message}");
        }
    }

    public async Task BookTicketAsync(int userId, int ticketId)
    {
        try
        {
            await userRepository.BookTicketAsync(userId, ticketId);
        }
        catch (Exception e)
        {
            throw new Exception($"Error occured while trying to book ticket: {e.Message}");
        }
    }

    public async  Task<List<BookingGetDto>> GetUserBookings(int userId)
    {
        try
        {
            var bookings = await userRepository.GetUserBookingsAsync(userId);
            return bookings.Select(userBooking => new BookingGetDto()
            {
                Id = userBooking.Id,
                UserId = userBooking.UserId,
                TicketId = userBooking.TicketId
            }).ToList();
        }
        catch (Exception e)
        {
            throw new Exception($"Error occured while trying to get user bookings: {e.Message}");
        }
    }

    public async Task<List<TicketGetDto>> GetUserTickets(int userId)
    {
        try
        {
            var tickets = await userRepository.GetUserTicketsAsync(userId);
            return tickets.Select(t => new TicketGetDto()
            {
                Id = t.Id,
                FestivalId = t.FestivalId,
                Name = t.Name,
                Price = t.Price,
                Quantity = t.Quantity,
                Type = t.Type
            }).ToList();
        }
        catch (Exception e)
        {
            throw new Exception($"Error occured while trying to get user {userId} tickets: {e.Message}");
        }
    }

    public async Task DeleteUserBookingAsync(int userId, int ticketId)
    {
        await userRepository.DeleteUserBookingAsync(userId, ticketId);
    }
}