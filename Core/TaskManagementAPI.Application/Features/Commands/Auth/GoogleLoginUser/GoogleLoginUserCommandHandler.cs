using AutoMapper;
using MediatR;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs.Authentication;
using TaskManagementAPI.Application.DTOs.Authentication.Google;

namespace TaskManagementAPI.Application.Features.Commands.Auth.GoogleLoginUser;

public class GoogleLoginUserCommandHandler : IRequestHandler<GoogleLoginUserCommandRequest, GoogleLoginUserCommandResponse>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public GoogleLoginUserCommandHandler(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<GoogleLoginUserCommandResponse> Handle(GoogleLoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        TokenDTO tokenDTO = await _authService.GoogleLoginAsync(_mapper.Map<GoogleLoginUserDTO>(request));
        return new()
        {
            AccessToken = tokenDTO.AccessToken,
            RefreshToken = tokenDTO.RefreshToken,
        };
    }
}
