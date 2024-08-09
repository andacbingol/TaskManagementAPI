using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagementAPI.Application.Features.Commands.Role.UpdateRole;
public class UpdateRoleCommandRequest : IRequest<UpdateRoleCommandResponse>
{
    [FromRoute(Name = "Id")]
    public Guid Id { get; set; }
    [FromBody]
    public UpdateRoleCommandRequestBody UpdateRoleCommandRequestBody { get; set; }
}

public class UpdateRoleCommandRequestBody
{
    public string Name { get; set; }
}
