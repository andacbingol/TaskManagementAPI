using TaskManagementAPI.Application.DTOs;
using TaskManagementAPI.Application.DTOs.Role;

namespace TaskManagementAPI.Application.Abstractions.Services;
public interface IRoleService
{
    Task<List<RoleDTO>> GetRolesAsync();
    Task<RoleDTO?> GetRoleByIdAsync(Guid id);
    Task CreateRoleAsync(string name);
    Task CreateRoleAsync(CreateRoleDTO createRoleDTO);
    Task<bool> UpdateRoleAsync(UpdateRoleDTO updateRoleDTO);
    Task DeleteRoleByIdAsync(Guid id);

    Task DeleteRoleByNameAsync(string roleName);
    Task<bool> RoleExistAsync(string roleName);
    Task<bool> RoleExistAsync(Guid id);
    Task<bool> IsRoleCriticalAsync(Guid id);
}
