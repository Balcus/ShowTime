using ShowTime.BusinessLogic.Dto.UserDto;

namespace ShowTime.BusinessLogic.Abstractions;

public interface IUserService
{
    Task CreateUserAsync(LoginDto userCreateDto);
    Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
}