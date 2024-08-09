using MediatR;

namespace TaskManagementAPI.Application.Features.Commands.Auth.FacebookLoginUser;

public class FacebookLoginUserCommandRequest : IRequest<FacebookLoginUserCommandResponse>
{
    public string AuthToken { get; set; }
}
