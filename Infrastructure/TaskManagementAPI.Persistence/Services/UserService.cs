using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs;
using TaskManagementAPI.Application.DTOs.User;
using TaskManagementAPI.Application.Features.Exceptions.AppInternalExceptions;
using TaskManagementAPI.Domain.Constants;
using TaskManagementAPI.Domain.Entities.Identity;
using TaskManagementAPI.Persistence.Extensions;

namespace TaskManagementAPI.Persistence.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IRoleService _roleService;
    private readonly IMapper _mapper;

    public UserService(UserManager<AppUser> userManager, IMapper mapper, IRoleService roleService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _roleService = roleService;
    }

    public async Task<CreateUserResultDTO> CreateUserAsync(CreateUserDTO createUserDTO)
    {
        AppUser appUser = _mapper.Map<AppUser>(createUserDTO);
        IdentityResult result = await _userManager.CreateAsync(appUser, createUserDTO.Password);

        if (!result.Succeeded)
        {
            string errorMessage = $"AppUser creation failed for '{createUserDTO.Email}'";
            throw new UserCreationException(errorMessage, result);
        }

        return _mapper.Map<CreateUserResultDTO>(appUser);
    }
    public async Task<bool> DeleteUserAsync(Guid id)
    {
        AppUser? appUser = await _userManager.FindByIdAsync(id.ToString());

        if (appUser is null)
            return false;

        IdentityResult result = await _userManager.DeleteAsync(appUser);
        if (!result.Succeeded)
        {
            string errorMessage = $"AppUser deletion failed for '{appUser.UserName}'";
            throw new UserDeletionException(errorMessage, result);
        }
        return true;
    }

    public async Task<UserDTO?> GetUserByIdAsync(Guid id)
    {
        AppUser? appUser = await _userManager.Users
            .Include(u => u.AppUserRoles)
            .ThenInclude(ur => ur.AppRole)
            .FirstOrDefaultAsync(u => u.Id == id);
        UserDTO? userDTO = _mapper.Map<UserDTO?>(appUser);
        return userDTO;
    }

    public async Task<List<UserDTO>> GetUsersAsync(UserFilterDTO userFilterDTO)
    {

        IQueryable<UserDTO> query = _userManager.Users.ApplyFilter(userFilterDTO)
       .ApplySorts(userFilterDTO)
       .ApplyPagination<AppUser>(userFilterDTO.Pagination)
       .Include(u => u.AppUserRoles)
       .ThenInclude(ur => ur.AppRole)
       .Select(u => _mapper.Map<UserDTO>(u));

        if (userFilterDTO.Roles is not null)
        {
            foreach (string role in userFilterDTO.Roles)
                if (!string.IsNullOrEmpty(role))
                    query = query.Where(u => u.Roles.Contains(role));
        }
        return await query.ToListAsync();
    }


    public async Task UpdatePasswordAsync(UpdatePasswordDTO updatePasswordDTO)
    {
        AppUser? appUser = await _userManager.FindByIdAsync(updatePasswordDTO.Id.ToString());

        if (appUser is null)
            throw new UserNotFoundException();

        IdentityResult result = await _userManager.ChangePasswordAsync(appUser, updatePasswordDTO.Password, updatePasswordDTO.PasswordNew);

        if (!result.Succeeded)
        {
            string errorMessage = $"Update password failed for '{appUser.UserName}'";
            throw new UpdatePasswordFailedException(errorMessage, result);
        }

        result = await _userManager.UpdateSecurityStampAsync(appUser);
        if (!result.Succeeded)
        {
            string errorMessage = $"Update password failed for '{appUser.UserName}' Security Stamp could not updated";
            throw new UpdatePasswordFailedException(errorMessage, result);
        }
    }
    public async Task<AppUser?> FindByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }
    public async Task<AppUser?> FindByIdAsync(Guid id)
    {
        return await _userManager.FindByIdAsync(id.ToString());
    }
    public async Task<IdentityResult> CreateAsync(AppUser appUser)
    {
        return await _userManager.CreateAsync(appUser);
    }
    public async Task<IdentityResult> AddLoginAsync(AppUser appUser, UserLoginInfo login)
    {
        return await _userManager.AddLoginAsync(appUser, login);
    }
    public Task<AppUser> FindByLoginAsync(string loginProvider, string providerKey)
    {
        return _userManager.FindByLoginAsync(loginProvider, providerKey);
    }
    public async Task UpdateUserAsync(AppUser appUser)
    {
        IdentityResult result = await _userManager.UpdateAsync(appUser);
        if (!result.Succeeded)
        {
            string errorMessage = $"Update User failed for '{appUser.UserName}'";
            throw new UpdateUserFailedException(errorMessage, result);
        }
    }
    public async Task UpdateSecurityStampAsync(AppUser appUser)
    {
        IdentityResult result = await _userManager.UpdateSecurityStampAsync(appUser);
        if (!result.Succeeded)
        {
            string errorMessage = $"Update security stamp failed for '{appUser.UserName}'";
            throw new UpdateSecurityStampFailedException(errorMessage, result);
        }
    }
    public async Task<string> GenerateEmailConfirmationTokenAsync(AppUser appUser)
    {
        return await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
    }
    public async Task<string> GeneratePasswordResetTokenAsync(AppUser appUser)
    {
        return await _userManager.GeneratePasswordResetTokenAsync(appUser);
    }
    public async Task<IdentityResult> ConfirmEmailAsync(AppUser appUser, string token)
    {
        return await _userManager.ConfirmEmailAsync(appUser, token);
    }
    public async Task<IdentityResult> ResetPasswordAsync(AppUser appUser, string token, string newPassword)
    {
        return await _userManager.ResetPasswordAsync(appUser, token, newPassword);
    }
    public async Task<IdentityResult> AddClaimsAsync(AppUser appUser, IEnumerable<Claim> claims)
    {
        return await _userManager.AddClaimsAsync(appUser, claims);
    }
    public async Task<IdentityResult> AddClaimAsync(AppUser appUser, Claim claim)
    {
        return await _userManager.AddClaimAsync(appUser, claim);
    }
    public async Task AssignDefaultRoleAsync(CreateUserDTO createUserDTO)
    {
        AppUser? appUser = await _userManager.FindByIdAsync(createUserDTO.Id.ToString());
        if (appUser is null)
            throw new UserNotFoundException();

        string defaultRole = Roles.AppUser;

        if (!await _roleService.RoleExistAsync(defaultRole))
            await _roleService.CreateRoleAsync(defaultRole);

        IdentityResult result = await _userManager.AddToRoleAsync(appUser, defaultRole);
        if (!result.Succeeded)
        {
            string errorMessage = $"Adding AppUser '{appUser.Email}' to role '{defaultRole}' failed!";
            throw new AddUserToRoleException(errorMessage, result);
        }
    }
    public async Task AddUserToRoleAsync(AppUser appUser, string role)
    {
        IdentityResult result = await _userManager.AddToRoleAsync(appUser, role);
        if (!result.Succeeded)
        {
            string errorMessage = $"Adding AppUser '{appUser.Email}' to role '{role}' failed!";
            throw new AddUserToRoleException(errorMessage, result);
        }
    }
    public async Task<AppUser?> FindByNameAsync(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }
    public async Task<bool> CheckPasswordAsync(AppUser appUser, string password)
    {
        return await _userManager.CheckPasswordAsync(appUser, password);
    }

    public async Task<IList<string>> GetUserRolesAsync(string email)
    {
        AppUser? appUser = await _userManager.FindByEmailAsync(email);
        return await _userManager.GetRolesAsync(appUser);
    }
    public async Task<IList<string>> GetUserRolesAsync(Guid id)
    {
        AppUser? appUser = await _userManager.FindByIdAsync(id.ToString());
        return await _userManager.GetRolesAsync(appUser);
    }
    public async Task<bool> IsInRoleAsync(Guid id, string role)
    {
        AppUser? appUser = await _userManager.FindByIdAsync(id.ToString());
        return await _userManager.IsInRoleAsync(appUser, role);
    }
    public async Task<bool> IsInRoleAsync(string email, string role)
    {
        AppUser? appUser = await _userManager.FindByEmailAsync(email.ToString());
        return await _userManager.IsInRoleAsync(appUser, role);
    }
    public async Task<bool> CheckIfEmailExistAsync(string email)
    {
        AppUser? appUser = await _userManager.FindByEmailAsync(email);
        return appUser is not null;
    }
    public async Task<bool> CheckIfUserNameExistAsync(string userName)
    {
        AppUser? appUser = await _userManager.FindByNameAsync(userName);
        return appUser is not null;
    }

    public async Task<List<UserDTO>> GetUsersInRoleAsync(string roleName)
    {
        IList<AppUser> appUsers = await _userManager.GetUsersInRoleAsync(roleName);
        return appUsers.Select(u => _mapper.Map<UserDTO>(u)).ToList();
    }
    public async Task<List<UserDTO>> GetUsersInRoleAsync(Guid id)
    {
        RoleDTO? roleDTO = await _roleService.GetRoleByIdAsync(id);

        IList<AppUser> appUsers = 
            roleDTO is null ? new List<AppUser>() 
            : await _userManager.GetUsersInRoleAsync(roleDTO?.Name);

        return appUsers.Select(u => _mapper.Map<UserDTO>(u)).ToList();
    }
}
