namespace ShowTime.DataAccess.Exceptions;

public class UserAlreadyExistsException(string email) : Exception($"User with email {email} already exists");