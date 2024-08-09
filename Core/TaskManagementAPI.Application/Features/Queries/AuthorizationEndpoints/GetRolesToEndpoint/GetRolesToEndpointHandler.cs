using MediatR;

namespace TaskManagementAPI.Application.Features.Queries.AuthorizationEndpoints.GetRolesToEndpoint;
public class GetRolesToEndpointHandler : IRequestHandler<GetRolesToEndpointRequest, GetRolesToEndpointResponse>
{
    public Task<GetRolesToEndpointResponse> Handle(GetRolesToEndpointRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
