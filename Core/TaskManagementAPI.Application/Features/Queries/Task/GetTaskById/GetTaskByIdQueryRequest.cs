using MediatR;

namespace TaskManagementAPI.Application.Features.Queries.Task.GetTaskById;
public class GetTaskByIdQueryRequest : IRequest<GetTaskByIdQueryResponse>
{
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid Id { get; set; }
}
