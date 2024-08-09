using MediatR;

namespace TaskManagementAPI.Application.Features.Commands.AuthorizationEndpoints.AssignRoleToEndpoints;
public class AssignRoleToEndpointsCommandRequest : IRequest<AssignRoleToEndpointsCommandResponse>
{
    public string[] Roles { get; set; }
    public string Code { get; set; }
}
