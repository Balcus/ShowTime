namespace ShowTime.DataAccess.Exceptions;

public class UserDoesntExistException() : Exception("The provided email does not correspond to an account")
{
    
}