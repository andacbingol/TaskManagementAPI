using MediatR;

namespace TaskManagementAPI.Application.Features.Commands.Auth.ConfirmPasswordReset;

public class ConfirmPasswordResetCommandRequest : IRequest<ConfirmPasswordResetCommandResponse>
{
    public Guid Id { get; set; }
    public string ResetToken { get; set; }
    public string Password { get; set; }
    public string PasswordConfirm { get; set; }
}
