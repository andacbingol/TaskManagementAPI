using MediatR;

namespace TaskManagementAPI.Application.Features.Queries.Role.GetRoles;
public class GetRolesQueryRequest : IRequest<List<GetRolesQueryResponse>>
{

}
