using Microsoft.AspNetCore.Identity;
using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Exceptions.AppInternalExceptions;

public class RoleCreationException : AppInternalBaseException
{
    public RoleCreationException(string message) : base(message) { }

    public RoleCreationException(string message, Exception innerException) : base(message, innerException) { }

    public RoleCreationException(string message, IdentityResult? identityResult = null) : base(message, identityResult) { }
}
