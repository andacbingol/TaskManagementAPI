using MediatR;

namespace TaskManagementAPI.Application.Features.Commands.Task.DeleteTask;
public class DeleteTaskCommandRequest : IRequest<DeleteTaskCommandResponse>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
}
