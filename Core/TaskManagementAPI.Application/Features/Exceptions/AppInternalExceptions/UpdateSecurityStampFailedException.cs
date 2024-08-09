using Microsoft.AspNetCore.Identity;
using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Exceptions.AppInternalExceptions;

public class UpdateSecurityStampFailedException : AppInternalBaseException
{
    public UpdateSecurityStampFailedException() : base("Update security stamp failed") { }

    public UpdateSecurityStampFailedException(string message) : base(message) { }

    public UpdateSecurityStampFailedException(string message, IdentityResult identityResult) : base(message, identityResult) { }

    public UpdateSecurityStampFailedException(string message, Exception innerException) : base(message, innerException) { }
}
