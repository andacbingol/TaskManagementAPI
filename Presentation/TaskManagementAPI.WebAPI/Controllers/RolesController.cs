using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Application.Features.Commands.Role.CreateRole;
using TaskManagementAPI.Application.Features.Commands.Role.DeleteRole;
using TaskManagementAPI.Application.Features.Commands.Role.UpdateRole;
using TaskManagementAPI.Application.Features.Queries.Role.GetRoleById;
using TaskManagementAPI.Application.Features.Queries.Role.GetRoles;
using TaskManagementAPI.Domain.Constants;
using TaskManagementAPI.WebAPI.Configurations;

namespace TaskManagementAPI.WebAPI.Controllers;
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin)]
    [HttpGet(ApiRoutes.Roles.GetRoles)]
    public async Task<IActionResult> GetRoles(GetRolesQueryRequest request)
    {
        List<GetRolesQueryResponse> response = await _mediator.Send(request);
        return Ok(response);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin)]
    [HttpGet(ApiRoutes.Roles.GetRoleById)]
    public async Task<IActionResult> GetRoleById(GetRoleByIdQueryRequest request)
    {
        GetRoleByIdQueryResponse response = await _mediator.Send(request);
        return response is null ? NoContent() : Ok(response);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin)]
    [HttpPost(ApiRoutes.Roles.CreateRole)]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommandRequest request)
    {
        CreateRoleCommandResponse response = await _mediator.Send(request);
        string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        string locationUri = baseUrl + "/" + ApiRoutes.Roles.GetRoleById.Replace("{Id}", response.Id.ToString());
        return Created(locationUri, response);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin)]
    [HttpPut(ApiRoutes.Roles.UpdateRole)]
    public async Task<IActionResult> UpdateRole(UpdateRoleCommandRequest request)
    {
        UpdateRoleCommandResponse response = await _mediator.Send(request);

        return response.Success ? Ok() : NotFound();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin)]
    [HttpDelete(ApiRoutes.Roles.DeleteRole)]
    public async Task<IActionResult> DeleteRole([FromRoute] DeleteRoleCommandRequest request)
    {
        DeleteRoleCommandResponse response = await _mediator.Send(request);

        return response.Succeeded ? NoContent() : NotFound();
    }
}
