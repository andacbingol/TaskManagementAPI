using AutoMapper;
using MediatR;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs.Authentication;

namespace TaskManagementAPI.Application.Features.Commands.Auth.ConfirmEmail;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommandRequest, ConfirmEmailCommandResponse>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    public ConfirmEmailCommandHandler(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }


    public async Task<ConfirmEmailCommandResponse> Handle(ConfirmEmailCommandRequest request, CancellationToken cancellationToken)
    {
        await _authService.ConfirmEmailAsync(_mapper.Map<ConfirmEmailTokenDTO>(request));
        return new();
    }
}
