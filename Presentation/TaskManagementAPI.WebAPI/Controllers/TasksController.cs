using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Application.Features.Commands.Task.CreateTask;
using TaskManagementAPI.Application.Features.Commands.Task.DeleteTask;
using TaskManagementAPI.Application.Features.Commands.Task.UpdateTask;
using TaskManagementAPI.Application.Features.Queries.Task.GetTaskById;
using TaskManagementAPI.Application.Features.Queries.Task.GetTasks;
using TaskManagementAPI.Domain.Constants;
using TaskManagementAPI.WebAPI.Configurations;

namespace TaskManagementAPI.WebAPI.Controllers;

[ApiController]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.AdminOrUser)]
    [HttpGet(ApiRoutes.Tasks.GetTasks)]
    public async Task<IActionResult> GetTasks([FromQuery] GetTasksQueryRequest request)
    {
        List<GetTasksQueryResponse> response = await _mediator.Send(request);

        return response.Count <= 0 ? NotFound() : Ok(response);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.AdminOrUser)]
    [HttpGet(ApiRoutes.Tasks.GetTaskById)]
    public async Task<IActionResult> GetTaskById([FromRoute] GetTaskByIdQueryRequest request)
    {
        GetTaskByIdQueryResponse response = await _mediator.Send(request);

        return response is null ? NotFound() : Ok(response);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.AdminOrUser)]
    [HttpPost(ApiRoutes.Tasks.CreateTask)]
    public async Task<IActionResult> CreateTask(CreateTaskCommandRequest request)
    {
        CreateTaskCommandResponse response = await _mediator.Send(request);

        if (response.CreateTaskDTO is null)
            return BadRequest(response.Errors);

        string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";

        string locationUri = baseUrl + "/" + ApiRoutes.Tasks.GetTaskById.Replace("{userId}", request.UserId.ToString()).Replace("{projectId}", request.ProjectId.ToString()).Replace("{Id}", response.CreateTaskDTO.Id.ToString());
        return Created(locationUri, response.CreateTaskDTO);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.AdminOrUser)]
    [HttpDelete(ApiRoutes.Tasks.DeleteTask)]
    public async Task<IActionResult> DeleteTask([FromRoute] DeleteTaskCommandRequest request)
    {
        DeleteTaskCommandResponse response = await _mediator.Send(request);

        return response.Succeeded ? NoContent() : NotFound();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.AdminOrUser)]
    [HttpPut(ApiRoutes.Tasks.UpdateTask)]
    public async Task<IActionResult> UpdateTask(UpdateTaskCommandRequest request)
    {
        UpdateTaskCommandResponse response = await _mediator.Send(request);

        return response.Success ? Ok() : NotFound();
    }
}
