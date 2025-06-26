using ShowTime.DataAccess.Enums;

namespace ShowTime.DataAccess.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.User;
    
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}