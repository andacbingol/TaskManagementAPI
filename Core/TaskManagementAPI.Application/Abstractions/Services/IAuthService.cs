using TaskManagementAPI.Application.Abstractions.Services.Authentication;

namespace TaskManagementAPI.Application.Abstractions.Services;

public interface IAuthService : IInternalAuthenticationService, IExternalAuthenticationService
{
}
