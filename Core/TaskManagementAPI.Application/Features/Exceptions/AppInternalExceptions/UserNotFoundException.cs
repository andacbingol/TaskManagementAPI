using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Exceptions.AppInternalExceptions;

public class UserNotFoundException : AppInternalBaseException
{
    public UserNotFoundException() : base("AppUser not found!") { }

    public UserNotFoundException(string message) : base(message)
    {
    }

    public UserNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
