using AutoMapper;
using MediatR;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs.Authentication;

namespace TaskManagementAPI.Application.Features.Commands.Auth.ConfirmPasswordReset;

public class ConfirmPasswordResetCommandHandler : IRequestHandler<ConfirmPasswordResetCommandRequest, ConfirmPasswordResetCommandResponse>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public ConfirmPasswordResetCommandHandler(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<ConfirmPasswordResetCommandResponse> Handle(ConfirmPasswordResetCommandRequest request, CancellationToken cancellationToken)
    {
        await _authService.PasswordResetAsync(_mapper.Map<PasswordResetDTO>(request));
        return new();
    }
}
