using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.Dto.UserDto;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Interfaces;

namespace ShowTime.BusinessLogic.Services;
// make an actual login/register component
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
        catch (Exception e)
        {
            throw new Exception($"Error while trying to add user {e.Message}");
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
        catch (Exception e)
        {
            // todo
            throw new Exception($"Error while trying to login: {e.Message}");
        }
    }
}