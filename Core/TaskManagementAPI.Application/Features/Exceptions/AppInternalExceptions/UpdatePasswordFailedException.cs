using Microsoft.AspNetCore.Identity;
using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Exceptions.AppInternalExceptions;

public class UpdatePasswordFailedException : AppInternalBaseException
{
    public UpdatePasswordFailedException() : base("Update password failed") { }

    public UpdatePasswordFailedException(string message) : base(message) { }

    public UpdatePasswordFailedException(string message, IdentityResult identityResult) : base(message, identityResult) { }

    public UpdatePasswordFailedException(string message, Exception innerException) : base(message, innerException) { }
}
