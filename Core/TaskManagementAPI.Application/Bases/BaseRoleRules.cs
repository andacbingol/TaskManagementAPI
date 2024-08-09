using TaskManagementAPI.Application.Abstractions.Services;

namespace TaskManagementAPI.Application.Bases;
public abstract class BaseRoleRules : BaseRules
{
    protected readonly IRoleService _roleService;

    protected BaseRoleRules(IRoleService roleService)
    {
        _roleService = roleService;
    }
}
