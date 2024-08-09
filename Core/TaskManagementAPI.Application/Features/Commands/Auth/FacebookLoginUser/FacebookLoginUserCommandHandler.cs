using AutoMapper;
using MediatR;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs.Authentication;
using TaskManagementAPI.Application.DTOs.Authentication.Facebook;

namespace TaskManagementAPI.Application.Features.Commands.Auth.FacebookLoginUser;

public class FacebookLoginUserCommandHandler : IRequestHandler<FacebookLoginUserCommandRequest, FacebookLoginUserCommandResponse>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public FacebookLoginUserCommandHandler(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<FacebookLoginUserCommandResponse> Handle(FacebookLoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        TokenDTO tokenDTO = await _authService.FacebookLoginAsync(_mapper.Map<FacebookLoginUserDTO>(request));
        return new()
        {
            AccessToken = tokenDTO.AccessToken,
            RefreshToken = tokenDTO.RefreshToken,
        };
    }
}
