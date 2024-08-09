using Microsoft.AspNetCore.Identity;
using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Exceptions.BusinessExceptions;

public class ExternalAuthenticationFailedException : BusinessBaseException
{
    public ExternalAuthenticationFailedException() : base("External authentication failed!") { }

    public ExternalAuthenticationFailedException(string message) : base(message) { }

    public ExternalAuthenticationFailedException(string message, IdentityResult identityResult) : base(message, identityResult) { }

    public ExternalAuthenticationFailedException(string message, Exception innerException) : base(message, innerException) { }
}
