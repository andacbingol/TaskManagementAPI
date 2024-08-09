using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Domain.Enums;

namespace TaskManagementAPI.Application.Features.Commands.Task.CreateTask;
public class CreateTaskCommandRequest : IRequest<CreateTaskCommandResponse>
{
    [FromRoute(Name ="userId")]
    public Guid UserId { get; set; }
    [FromRoute(Name = "projectId")]
    public Guid ProjectId { get; set; }
    [FromBody]
    public CreateTaskCommandRequestBody CreateTaskCommandRequestBody { get; set; }
}

public class CreateTaskCommandRequestBody
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public DateTime DueDate { get; set; }
}
