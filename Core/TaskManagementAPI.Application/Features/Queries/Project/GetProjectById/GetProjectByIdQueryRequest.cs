using MediatR;

namespace TaskManagementAPI.Application.Features.Queries.Project.GetProjectById;

public class GetProjectByIdQueryRequest : IRequest<GetProjectByIdQueryResponse>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}
