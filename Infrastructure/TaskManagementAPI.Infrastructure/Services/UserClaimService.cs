using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.Features.Exceptions.AppInternalExceptions;

namespace TaskManagementAPI.Infrastructure.Services;

public class UserClaimService : IUserClaimService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserClaimService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetClaimUserId()
    {
        var userIdClaim = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (Guid.TryParse(userIdClaim, out var userId))
        {
            return userId;
        }
        throw new UserNotFoundException();
    }
}
