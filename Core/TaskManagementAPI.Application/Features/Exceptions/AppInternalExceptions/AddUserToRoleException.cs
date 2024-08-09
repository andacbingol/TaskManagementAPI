using Microsoft.AspNetCore.Identity;
using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Exceptions.AppInternalExceptions;

public class AddUserToRoleException : AppInternalBaseException
{
    public AddUserToRoleException() : base("Add AppUser to Role failed!") { }

    public AddUserToRoleException(string message) : base(message) { }

    public AddUserToRoleException(string message, IdentityResult identityResult) : base(message, identityResult) { }

    public AddUserToRoleException(string message, Exception innerException) : base(message, innerException) { }
}
