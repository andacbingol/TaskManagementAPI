using MediatR;

namespace TaskManagementAPI.Application.Features.Commands.AuthorizationEndpoints.AssignRoleToEndpoints;
public class AssignRoleToEndpointsCommandHandler : IRequestHandler<AssignRoleToEndpointsCommandRequest, AssignRoleToEndpointsCommandResponse>
{
    public Task<AssignRoleToEndpointsCommandResponse> Handle(AssignRoleToEndpointsCommandRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
