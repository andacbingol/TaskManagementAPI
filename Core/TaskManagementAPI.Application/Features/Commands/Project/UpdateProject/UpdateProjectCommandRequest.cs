using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagementAPI.Application.Features.Commands.Project.UpdateProject;

public class UpdateProjectCommandRequest : IRequest<UpdateProjectCommandResponse>
{
    [FromRoute(Name = "Id")]
    public Guid Id { get; set; }
    [FromRoute(Name = "userId")]
    public Guid UserId { get; set; }
    [FromBody]
    public UpdateProjectCommandRequestBody UpdateProjectCommandRequestBody { get; set; }
}
public class UpdateProjectCommandRequestBody
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
