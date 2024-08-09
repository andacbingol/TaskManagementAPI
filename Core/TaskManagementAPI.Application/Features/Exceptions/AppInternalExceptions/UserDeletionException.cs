using Microsoft.AspNetCore.Identity;
using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Exceptions.AppInternalExceptions;

public class UserDeletionException : AppInternalBaseException
{
    public UserDeletionException(string message) : base(message) { }

    public UserDeletionException(string message, Exception innerException) : base(message, innerException) { }

    public UserDeletionException(string message, IdentityResult identityResult) : base(message, identityResult) { }
}
