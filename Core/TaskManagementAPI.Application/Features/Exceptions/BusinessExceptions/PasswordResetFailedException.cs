using Microsoft.AspNetCore.Identity;
using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Exceptions.BusinessExceptions;

public class PasswordResetFailedException : BusinessBaseException
{
    public PasswordResetFailedException() : base("Password reset failed!") { }

    public PasswordResetFailedException(string message) : base(message) { }

    public PasswordResetFailedException(string message, IdentityResult identityResult) : base(message, identityResult) { }

    public PasswordResetFailedException(string message, Exception innerException) : base(message, innerException) { }
}
