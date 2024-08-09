using MediatR;

namespace TaskManagementAPI.Application.Features.Commands.User.DeleteUser;

public class DeleteUserCommandRequest : IRequest<DeleteUserCommandResponse>
{
    public Guid Id { get; set; }
}
