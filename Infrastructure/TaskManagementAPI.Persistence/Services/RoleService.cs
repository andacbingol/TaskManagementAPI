using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs;
using TaskManagementAPI.Application.DTOs.Role;
using TaskManagementAPI.Application.Features.Exceptions.AppInternalExceptions;
using TaskManagementAPI.Domain.Constants;
using TaskManagementAPI.Domain.Entities.Identity;
using Task = System.Threading.Tasks.Task;

namespace TaskManagementAPI.Persistence.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;

    public RoleService(RoleManager<AppRole> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }
    public async Task<List<RoleDTO>> GetRolesAsync()
    {
        return await _roleManager.Roles
            .Select(r => _mapper.Map<RoleDTO>(r))
            .ToListAsync();
    }
    public async Task<RoleDTO?> GetRoleByIdAsync(Guid id)
    {
        AppRole? appRole = await _roleManager.Roles.SingleOrDefaultAsync(r => r.Id == id);
        return _mapper.Map<RoleDTO>(appRole);
    }

    public async Task CreateRoleAsync(string name)
    {
        IdentityResult result = await _roleManager.CreateAsync(new()
        {
            Id = Guid.NewGuid(),
            Name = name
        });

        if (!result.Succeeded)
        {
            string errorMessage = $"Role creation failed for '{name}'";
            throw new RoleCreationException(errorMessage, result);
        }
    }
    public async Task CreateRoleAsync(CreateRoleDTO createRoleDTO)
    {
        IdentityResult result = await _roleManager.CreateAsync(_mapper.Map<AppRole>(createRoleDTO));

        if (!result.Succeeded)
        {
            string errorMessage = $"Role creation failed for '{createRoleDTO.Name}'";
            throw new RoleCreationException(errorMessage, result);
        }
    }
    public async Task<bool> UpdateRoleAsync(UpdateRoleDTO updateRoleDTO)
    {
        AppRole? appRole = await _roleManager.FindByIdAsync(updateRoleDTO.Id.ToString());

        if (appRole is null)
            return false;

        if (!CheckIfThereIsChange(appRole, updateRoleDTO))
            return true;

        _mapper.Map(updateRoleDTO, appRole);

        IdentityResult identityResult = await _roleManager.UpdateAsync(appRole);

        if (!identityResult.Succeeded)
        {
            string errorMessage = $"Role update failed for '{updateRoleDTO.Name}'";
            throw new UpdateRoleFailedException(errorMessage, identityResult);
        }

        return true;
    }

    public async Task DeleteRoleByIdAsync(Guid id)
    {
        AppRole? appRole = await _roleManager.FindByIdAsync(id.ToString());

        if (appRole is null)
            throw new RoleDeletionException($"Role deletion failed for {id} : Role not found!");

        IdentityResult result = await _roleManager.DeleteAsync(appRole);
        if (!result.Succeeded)
        {
            string errorMessage = $"Role deletion failed for role id: '{id}'";
            throw new RoleDeletionException(errorMessage, result);
        }
    }

    public async Task DeleteRoleByNameAsync(string roleName)
    {
        AppRole? appRole = await _roleManager.FindByNameAsync(roleName);

        if (appRole is null)
            throw new RoleDeletionException($"Role deletion failed for {roleName}! Role not found!");

        IdentityResult result = await _roleManager.DeleteAsync(appRole);
        if (!result.Succeeded)
        {
            string errorMessage = $"Role deletion failed for role id: '{roleName}'";
            throw new RoleDeletionException(errorMessage, result);
        }
    }
    public async Task<bool> RoleExistAsync(string roleName)
    {
        return await _roleManager.RoleExistsAsync(roleName);
    }
    public async Task<bool> RoleExistAsync(Guid id)
    {
        AppRole? appRole = await _roleManager.FindByIdAsync(id.ToString());
        return appRole is not null;
    }

    private bool CheckIfThereIsChange(AppRole appRole, UpdateRoleDTO updateRoleDTO) =>
        !(updateRoleDTO.Name.Equals(appRole.Name));
    public async Task<bool> IsRoleCriticalAsync(Guid id)
    {
        AppRole? appRole = await _roleManager.FindByIdAsync(id.ToString());

        return appRole is null ? false : (appRole.Name.Equals(Roles.Admin) || appRole.Name.Equals(Roles.AppUser));
    }
}
