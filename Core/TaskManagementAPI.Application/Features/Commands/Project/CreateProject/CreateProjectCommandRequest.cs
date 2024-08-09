using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagementAPI.Application.Features.Commands.Project.CreateProject;

public class CreateProjectCommandRequest : IRequest<CreateProjectCommandResponse>
{
    [FromRoute(Name = "userId")]
    public Guid UserId { get; set; }
    [FromBody]
    public CreateProjectCommandRequestBody CreateProjectCommandRequestBody { get; set; }
}
public class CreateProjectCommandRequestBody
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
