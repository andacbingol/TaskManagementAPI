using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TaskManagementAPI.Application.DTOs.Authentication;
using TaskManagementAPI.Application.DTOs.User;
using TaskManagementAPI.Domain.Entities.Identity;

namespace TaskManagementAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResultDTO> CreateUserAsync(CreateUserDTO createUserDTO);
        Task UpdatePasswordAsync(UpdatePasswordDTO updatePasswordDTO);
        Task<List<UserDTO>> GetUsersAsync(UserFilterDTO userFilterDTO);
        Task<UserDTO?> GetUserByIdAsync(Guid id);
        Task<bool> DeleteUserAsync(Guid id);
        Task<AppUser?> FindByEmailAsync(string email);
        Task<AppUser?> FindByIdAsync(Guid id);
        Task<AppUser?> FindByNameAsync(string userName);
        Task<IdentityResult> CreateAsync(AppUser appUser);
        Task<IdentityResult> AddLoginAsync(AppUser appUser, UserLoginInfo login);
        Task<AppUser> FindByLoginAsync(string loginProvider, string providerKey);
        Task<bool> CheckPasswordAsync(AppUser appUser, string password);
        Task UpdateUserAsync(AppUser user);
        Task UpdateSecurityStampAsync(AppUser appUser);
        Task<string> GenerateEmailConfirmationTokenAsync(AppUser appUser);
        Task<string> GeneratePasswordResetTokenAsync(AppUser appUser);
        Task<IdentityResult> ConfirmEmailAsync(AppUser appUser, string token);
        Task<IdentityResult> ResetPasswordAsync(AppUser appUser, string token, string newPassword);
        Task<IdentityResult> AddClaimsAsync(AppUser appUser, IEnumerable<Claim> claims);
        Task<IdentityResult> AddClaimAsync(AppUser appUser, Claim claim);
        Task AssignDefaultRoleAsync(CreateUserDTO createUserDTO);
        Task AddUserToRoleAsync(AppUser appUser, string role);
        Task<List<UserDTO>> GetUsersInRoleAsync(string roleName);
        Task<List<UserDTO>> GetUsersInRoleAsync(Guid id);
        Task<IList<string>> GetUserRolesAsync(string email);
        Task<IList<string>> GetUserRolesAsync(Guid id);
        Task<bool> IsInRoleAsync(Guid id, string role);
        Task<bool> IsInRoleAsync(string email, string role);
        Task<bool> CheckIfEmailExistAsync(string email);
        Task<bool> CheckIfUserNameExistAsync(string userName);
    }
}
