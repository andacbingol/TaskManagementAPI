using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.Features.Exceptions.AuthorizationExceptions;
using TaskManagementAPI.Domain.Constants;

namespace TaskManagementAPI.Application.Bases;

public abstract class BaseUserRules : BaseRules
{
    protected readonly IUserService _userService;

    protected BaseUserRules(IUserService userService, string checkAdminOrAccountOwnerMessage)
    {
        _userService = userService;
        CheckAdminOrAccountOwnerMessage = checkAdminOrAccountOwnerMessage;
    }

    protected string CheckAdminOrAccountOwnerMessage { get; set; }

    public async Task CheckAdminOrAccountOwner(Guid claimUserId, Guid userId)
    {
        if (!await _userService.IsInRoleAsync(claimUserId, Roles.Admin) && claimUserId != userId)
        {
            throw new AuthorizationException(CheckAdminOrAccountOwnerMessage);
        }
    }
}
