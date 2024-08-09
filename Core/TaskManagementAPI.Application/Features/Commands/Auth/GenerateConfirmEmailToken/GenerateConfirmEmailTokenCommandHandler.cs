using MediatR;
using TaskManagementAPI.Application.Abstractions.Services;

namespace TaskManagementAPI.Application.Features.Commands.Auth.GenerateConfirmEmailToken;

public class GenerateConfirmEmailTokenCommandHandler : IRequestHandler<GenerateConfirmEmailTokenCommandRequest, GenerateConfirmEmailTokenCommandResponse>
{
    private readonly IAuthService _authService;

    public GenerateConfirmEmailTokenCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<GenerateConfirmEmailTokenCommandResponse> Handle(GenerateConfirmEmailTokenCommandRequest request, CancellationToken cancellationToken)
    {
        await _authService.GenerateEmailConfirmationTokenAsync(request.Email);
        return new();
    }
}
