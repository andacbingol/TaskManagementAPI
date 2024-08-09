using Microsoft.AspNetCore.Identity;
using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Exceptions.AppInternalExceptions;

public class UpdateUserFailedException : AppInternalBaseException
{
    public UpdateUserFailedException() : base("Update User failed!") { }

    public UpdateUserFailedException(string message) : base(message) { }

    public UpdateUserFailedException(string message, IdentityResult identityResult) : base(message, identityResult) { }

    public UpdateUserFailedException(string message, Exception innerException) : base(message, innerException) { }
}
