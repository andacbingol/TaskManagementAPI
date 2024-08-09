using Microsoft.AspNetCore.Identity;
using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Exceptions.BusinessExceptions;

public class LoginUserAuthenticationFailedException : BusinessBaseException
{
    public LoginUserAuthenticationFailedException() : base("Authentication failed!") { }

    public LoginUserAuthenticationFailedException(string message) : base(message) { }

    public LoginUserAuthenticationFailedException(string message, IdentityResult identityResult) : base(message, identityResult) { }

    public LoginUserAuthenticationFailedException(string message, Exception innerException) : base(message, innerException) { }
}
