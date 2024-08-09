using System.Security.Claims;
using TaskManagementAPI.Application.DTOs.Authentication;
using TaskManagementAPI.Domain.Entities.Identity;

namespace TaskManagementAPI.Application.Abstractions.Services;

public interface ITokenService
{
    Task<TokenDTO> CreateAccessTokenAsync(AppUser appUser, IList<string> roles);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
