using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.Bases;
using TaskManagementAPI.Application.Features.Exceptions.BusinessExceptions;

namespace TaskManagementAPI.Application.Features.Rules.Role;
public class CreateRoleRules : BaseRoleRules
{
    public CreateRoleRules(IRoleService roleService) : base(roleService)
    {
    }

    public async System.Threading.Tasks.Task RoleNameAlreadyExist(string roleName)
    {
        var exist = await _roleService.RoleExistAsync(roleName);
        if (exist)
            throw new ResourceConflictException("Role with the same name already exist.");
    }
    public async System.Threading.Tasks.Task RoleIdAlreadyExist(Guid id)
    {
        var exist = await _roleService.RoleExistAsync(id);
        if(exist)
            throw new ResourceConflictException("Role with the same Id already exist.");
    }
}
