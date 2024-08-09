using Microsoft.AspNetCore.Identity;
using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Exceptions.BusinessExceptions;

public class UserNotFoundException : BusinessBaseException
{
    public UserNotFoundException() : base("User not found!") { }

    public UserNotFoundException(string message) : base(message) { }

    public UserNotFoundException(string message, Exception innerException) : base(message, innerException) { }

    public UserNotFoundException(string message, IdentityResult identityResult) : base(message, identityResult) { }
}
