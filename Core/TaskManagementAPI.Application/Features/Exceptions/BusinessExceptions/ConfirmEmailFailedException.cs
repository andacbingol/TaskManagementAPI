using Microsoft.AspNetCore.Identity;
using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Exceptions.BusinessExceptions;

public class ConfirmEmailFailedException : BusinessBaseException
{
    public ConfirmEmailFailedException() : base("Email confirmation failed!") { }

    public ConfirmEmailFailedException(string message) : base(message) { }

    public ConfirmEmailFailedException(string message, IdentityResult identityResult) : base(message, identityResult) { }

    public ConfirmEmailFailedException(string message, Exception innerException) : base(message, innerException) { }
}
