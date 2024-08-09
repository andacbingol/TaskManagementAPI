using Microsoft.AspNetCore.Identity;
using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Exceptions.AppInternalExceptions;

public class UserCreationException : AppInternalBaseException
{
    public UserCreationException(string message) : base(message) { }

    public UserCreationException(string message, Exception innerException) : base(message, innerException) { }

    public UserCreationException(string message, IdentityResult identityResult) : base(message, identityResult) { }
}
