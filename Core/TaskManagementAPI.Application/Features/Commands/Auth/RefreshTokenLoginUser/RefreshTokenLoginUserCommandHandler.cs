using AutoMapper;
using MediatR;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs.Authentication;

namespace TaskManagementAPI.Application.Features.Commands.Auth.RefreshTokenLoginUser;

public class RefreshTokenLoginUserCommandHandler : IRequestHandler<RefreshTokenLoginUserCommandRequest, RefreshTokenLoginUserCommandResponse>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public RefreshTokenLoginUserCommandHandler(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<RefreshTokenLoginUserCommandResponse> Handle(RefreshTokenLoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        TokenDTO tokenDTO = await _authService.RefreshTokenLoginAsync(_mapper.Map<RefreshTokenLoginUserDTO>(request));
        return _mapper.Map<RefreshTokenLoginUserCommandResponse>(tokenDTO);
    }
}
