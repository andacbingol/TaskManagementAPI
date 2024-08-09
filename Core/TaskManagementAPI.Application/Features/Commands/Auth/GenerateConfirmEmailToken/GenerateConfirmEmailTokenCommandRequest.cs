using MediatR;

namespace TaskManagementAPI.Application.Features.Commands.Auth.GenerateConfirmEmailToken;

public class GenerateConfirmEmailTokenCommandRequest : IRequest<GenerateConfirmEmailTokenCommandResponse>
{
    public string Email { get; set; }
}
