using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Domain.Enums;

namespace TaskManagementAPI.Application.Features.Commands.Task.UpdateTask;
public class UpdateTaskCommandRequest : IRequest<UpdateTaskCommandResponse>
{
    [FromRoute(Name = "userId")]
    public Guid UserId { get; set; }
    [FromRoute(Name = "projectId")]
    public Guid ProjectId { get; set; }

    [FromRoute(Name = "Id")]
    public Guid Id { get; set; }
    [FromBody]
    public UpdateTaskCommandRequestBody UpdateTaskCommandRequestBody { get; set; }
}

public class UpdateTaskCommandRequestBody
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; }
    public DateTime DueDate { get; set; }
}
