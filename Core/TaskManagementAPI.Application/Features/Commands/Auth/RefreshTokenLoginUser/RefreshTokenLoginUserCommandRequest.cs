using MediatR;

namespace TaskManagementAPI.Application.Features.Commands.Auth.RefreshTokenLoginUser;

public class RefreshTokenLoginUserCommandRequest : IRequest<RefreshTokenLoginUserCommandResponse>
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
