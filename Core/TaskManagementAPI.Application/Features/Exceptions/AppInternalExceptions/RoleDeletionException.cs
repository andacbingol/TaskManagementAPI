using Microsoft.AspNetCore.Identity;
using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Exceptions.AppInternalExceptions;
public class RoleDeletionException : AppInternalBaseException
{
    public RoleDeletionException(string message) : base(message) { }

    public RoleDeletionException(string message, Exception innerException) : base(message, innerException) { }

    public RoleDeletionException(string message, IdentityResult? identityResult = null) : base(message, identityResult) { }
}
