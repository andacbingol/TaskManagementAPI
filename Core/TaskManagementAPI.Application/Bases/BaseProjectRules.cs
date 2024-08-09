using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.Features.Exceptions.AuthorizationExceptions;
using TaskManagementAPI.Domain.Constants;

namespace TaskManagementAPI.Application.Bases;

public abstract class BaseProjectRules :BaseRules
{
    protected readonly IUserService _userService;
    protected string CheckAdminOrAccountOwnerMessage { get; set; }

    protected BaseProjectRules(IUserService userService, string checkAdminOrAccountOwnerMessage)
    {
        _userService = userService;
        CheckAdminOrAccountOwnerMessage = checkAdminOrAccountOwnerMessage;
    }
    public async Task CheckAdminOrAccountOwnerAsync(Guid claimUserId, Guid userId)
    {
        if (!await _userService.IsInRoleAsync(claimUserId, Roles.Admin) && claimUserId != userId)
        {
            throw new AuthorizationException(CheckAdminOrAccountOwnerMessage);
        }
    }
}
