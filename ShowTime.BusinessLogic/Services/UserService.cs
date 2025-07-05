using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.Dto.UserDto;
using ShowTime.DataAccess.Enums;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Interfaces;

namespace ShowTime.BusinessLogic.Services;
// make an actual login/register component
public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task CreateUserAsync(LoginDto userCreateDto)
    {
        try
        {
            var createUser = new User()
            {
                Email = userCreateDto.Email,
                Password = userCreateDto.Password,
            };
            await userRepository.CreateAsync(createUser);
        }
        catch (Exception e)
        {
            throw new Exception($"Error while trying to add user {e.Message}");
        }
    }

    public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
    {
        return new LoginResponseDto()
        {
            Role = UserRole.Admin,
        };
    }
}