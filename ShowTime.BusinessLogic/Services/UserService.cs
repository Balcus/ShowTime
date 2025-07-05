using ShowTime.BusinessLogic.Abstractions;
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
        catch (Exception e)
        {
            // todo
            throw new Exception($"Error while trying to login: {e.Message}");
        }
    }
}