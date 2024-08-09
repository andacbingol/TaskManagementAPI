using MediatR;

namespace TaskManagementAPI.Application.Features.Commands.Auth.ConfirmEmail;

public class ConfirmEmailCommandRequest : IRequest<ConfirmEmailCommandResponse>
{
    public Guid Id { get; set; }
    public string ConfirmEmailToken { get; set; }
}
