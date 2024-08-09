using MediatR;

namespace TaskManagementAPI.Application.Features.Queries.User.GetUserById;

public class GetUserByIdQueryRequest : IRequest<GetUserByIdQueryResponse>
{
    public Guid Id { get; set; }
}
