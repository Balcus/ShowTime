using System.ComponentModel.DataAnnotations;

namespace ShowTime.BusinessLogic.Dto.UserDto;

public class LoginDto
{
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}