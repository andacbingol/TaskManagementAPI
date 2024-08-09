using MediatR;

namespace TaskManagementAPI.Application.Features.Commands.Auth.Login;

public class LoginCommandRequest : IRequest<LoginCommandResponse>
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
}
