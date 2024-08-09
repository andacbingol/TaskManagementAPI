using AutoMapper;
using MediatR;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs.Authentication;

namespace TaskManagementAPI.Application.Features.Commands.Auth.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, LoginCommandResponse>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public LoginCommandHandler(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
    {
        var token = await _authService.LoginAsync(_mapper.Map<LoginUserDTO>(request));
        return _mapper.Map<LoginCommandResponse>(token);
    }
}
