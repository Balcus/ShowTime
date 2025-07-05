using ShowTime.BusinessLogic.Dto.UserDto;

namespace ShowTime.BusinessLogic.Abstractions;

public interface IUserService
{
    Task RegisterUserAsync(LoginDto userCreateDto);
    Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
}