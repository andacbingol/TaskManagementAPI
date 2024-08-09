using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.Bases;
using TaskManagementAPI.Application.DTOs;
using TaskManagementAPI.Application.DTOs.User;
using TaskManagementAPI.Application.Features.Exceptions.AuthorizationExceptions;
using TaskManagementAPI.Application.Features.Exceptions.BusinessExceptions;
using TaskManagementAPI.Domain.Constants;

namespace TaskManagementAPI.Application.Features.Rules.Role;
public class DeleteRoleRules : BaseRoleRules
{
    private readonly IUserService _userService;
    public DeleteRoleRules(IRoleService roleService, IUserService userService) : base(roleService)
    {
        _userService = userService;
    }

    public async System.Threading.Tasks.Task CheckRoleIsCriticalAsync(Guid id)
    {
        if(await _roleService.IsRoleCriticalAsync(id))
            throw new ResourceConflictException("This role cannot be deleted.");
    }

    public async System.Threading.Tasks.Task CheckIfRoleIsInUseAsync(Guid id)
    {
        List<UserDTO> userDTOs = await _userService.GetUsersInRoleAsync(id);

        if (userDTOs.Count > 0)
            throw new ResourceConflictException("Role is assigned to one or more users. Please reassign users before deleting the role.");
    }
}
