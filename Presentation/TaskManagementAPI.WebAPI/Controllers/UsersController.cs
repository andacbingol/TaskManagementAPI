using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Application.Consts;
using TaskManagementAPI.Application.Features.Commands.User.CreateUser;
using TaskManagementAPI.Application.Features.Commands.User.DeleteUser;
using TaskManagementAPI.Application.Features.Commands.User.UpdatePassword;
using TaskManagementAPI.Application.Features.Queries.User.GetUserById;
using TaskManagementAPI.Application.Features.Queries.User.GetUsers;
using TaskManagementAPI.Domain.Constants;
using TaskManagementAPI.WebAPI.Configurations;

namespace TaskManagementAPI.WebAPI.Controllers;

[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin)]
    [HttpGet(ApiRoutes.Users.GetUsers)]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersQueryRequest request)
    {
        List<GetUsersQueryResponse> response = await _mediator.Send(request);

        return response.Count <= 0 ? NotFound() : Ok(response);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.AdminOrUser)]
    [HttpGet(ApiRoutes.Users.GetUserById)]
    public async Task<IActionResult> GetUserById([FromRoute] GetUserByIdQueryRequest request)
    {
        GetUserByIdQueryResponse? response = await _mediator.Send(request);
        return response is not null ? Ok(response) : NotFound();
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Users.CreateUser)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommandRequest request)
    {
        CreateUserCommandResponse response = await _mediator.Send(request);
        string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        string locationUri = baseUrl + "/" + ApiRoutes.Users.GetUserById.Replace("{Id}", response.Id.ToString());
        return Created(locationUri, response);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin)]
    [HttpDelete(ApiRoutes.Users.DeleteUser)]
    public async Task<IActionResult> DeleteUser([FromRoute] DeleteUserCommandRequest request)
    {
        DeleteUserCommandResponse response = await _mediator.Send(request);

        return response.Succeeded == true ? NoContent() : NotFound();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.AdminOrUser)]
    [HttpPost(ApiRoutes.Users.UpdatePassword)]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest request)
    {
        await _mediator.Send(request);
        return Ok();
    }
}
