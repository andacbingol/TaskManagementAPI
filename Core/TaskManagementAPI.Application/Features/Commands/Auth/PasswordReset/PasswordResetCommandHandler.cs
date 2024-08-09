using MediatR;
using TaskManagementAPI.Application.Abstractions.Services;

namespace TaskManagementAPI.Application.Features.Commands.Auth.PasswordReset;

public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommandRequest, PasswordResetCommandResponse>
{
    private readonly IAuthService _authService;

    public PasswordResetCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<PasswordResetCommandResponse> Handle(PasswordResetCommandRequest request, CancellationToken cancellationToken)
    {
        await _authService.GeneratePasswordResetTokenAsync(request.Email);
        return new();
    }
}
