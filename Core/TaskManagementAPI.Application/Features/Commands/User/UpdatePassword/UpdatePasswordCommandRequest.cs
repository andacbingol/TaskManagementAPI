using MediatR;

namespace TaskManagementAPI.Application.Features.Commands.User.UpdatePassword;

public class UpdatePasswordCommandRequest : IRequest<Unit>
{
    public string Password { get; set; }
    public string PasswordNew { get; set; }
    public string PasswordNewConfirm { get; set; }
}
