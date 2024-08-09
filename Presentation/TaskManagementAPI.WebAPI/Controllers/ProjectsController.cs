using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Application.Features.Commands.Project.CreateProject;
using TaskManagementAPI.Application.Features.Commands.Project.DeleteProject;
using TaskManagementAPI.Application.Features.Commands.Project.UpdateProject;
using TaskManagementAPI.Application.Features.Queries.Project.GetProjectById;
using TaskManagementAPI.Application.Features.Queries.Project.GetProjects;
using TaskManagementAPI.Domain.Constants;
using TaskManagementAPI.WebAPI.Configurations;

namespace TaskManagementAPI.WebAPI.Controllers;

[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.AdminOrUser)]
    [HttpGet(ApiRoutes.Projects.GetProjects)]
    public async Task<IActionResult> GetProjects([FromQuery] GetProjectsQueryRequest request)
    {
        var response = await _mediator.Send(request);

        if (response.Count <= 0)
            return NotFound();

        return Ok(response);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.AdminOrUser)]
    [HttpGet(ApiRoutes.Projects.GetProjectById)]
    public async Task<IActionResult> GetProjectById([FromRoute] GetProjectByIdQueryRequest getProjectByIdQueryRequest)
    {
        GetProjectByIdQueryResponse? response = await _mediator.Send(getProjectByIdQueryRequest);

        if (response is null)
            return NotFound();

        return Ok(response);

    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.AdminOrUser)]
    [HttpPost(ApiRoutes.Projects.CreateProject)]
    public async Task<IActionResult> CreateProject(CreateProjectCommandRequest createProjectCommandRequest)
    {
        CreateProjectCommandResponse response = await _mediator.Send(createProjectCommandRequest);
        if (response.CreateProjectDTO is null)
            return BadRequest(response.Errors);


        string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        string locationUri = baseUrl + "/" + ApiRoutes.Projects.GetProjectById.Replace("{userId}", createProjectCommandRequest.UserId.ToString()).Replace("{Id}", response.CreateProjectDTO.Id.ToString());
        return Created(locationUri, response.CreateProjectDTO);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.AdminOrUser)]
    [HttpDelete(ApiRoutes.Projects.DeleteProject)]
    public async Task<IActionResult> DeleteProject([FromRoute] DeleteProjectCommandRequest deleteProjectCommandRequest)
    {
        DeleteProjectCommandResponse response = await _mediator.Send(deleteProjectCommandRequest);
        if (response.Succeeded)
            return NoContent();
        return NotFound();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.AdminOrUser)]
    [HttpPut(ApiRoutes.Projects.UpdateProject)]
    public async Task<IActionResult> UpdateProject(UpdateProjectCommandRequest updateProjectCommandRequest)
    {
        UpdateProjectCommandResponse response = await _mediator.Send(updateProjectCommandRequest);
        if (response.Success)
            return Ok();
        return NotFound();
    }
}
