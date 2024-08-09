using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.Features.Exceptions.AuthorizationExceptions;
using TaskManagementAPI.Domain.Constants;

namespace TaskManagementAPI.Application.Bases;
public abstract class BaseTaskRules : BaseRules
{
    protected readonly IUserService _userService;
    protected readonly IProjectService _projectService;
    protected string CheckAdminOrAccountOwnerAsyncMessage { get; set; }
    protected string CheckUserIsProjectOwnerAsyncMessage { get; set; }

    protected BaseTaskRules(IUserService userService, IProjectService projectService, string checkAdminOrAccountOwnerMessage, string checkUserIsProjectOwnerAsyncMessage)
    {
        _userService = userService;
        _projectService = projectService;
        CheckAdminOrAccountOwnerAsyncMessage = checkAdminOrAccountOwnerMessage;
        CheckUserIsProjectOwnerAsyncMessage = checkUserIsProjectOwnerAsyncMessage;
    }

    public async Task CheckAdminOrAccountOwnerAsync(Guid claimUserId, Guid userId)
    {
        if (!await _userService.IsInRoleAsync(claimUserId, Roles.Admin) && claimUserId != userId)
            throw new AuthorizationException(CheckAdminOrAccountOwnerAsyncMessage);
    }

    public async Task CheckUserIsProjectOwnerAsync(Guid projectId, Guid userId)
    {
        if (!await _projectService.IsUserIsProjectOwnerAsync(projectId, userId))
            throw new AuthorizationException(CheckUserIsProjectOwnerAsyncMessage);
    }
}
