using Microsoft.AspNetCore.Identity;
using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Exceptions.AppInternalExceptions;
public class UpdateRoleFailedException : AppInternalBaseException
{
    public UpdateRoleFailedException(string message, IdentityResult identityResult) : base(message, identityResult)
    {
    }
}
