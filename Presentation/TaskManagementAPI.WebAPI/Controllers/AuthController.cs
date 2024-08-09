using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Application.Features.Commands.Auth.ConfirmEmail;
using TaskManagementAPI.Application.Features.Commands.Auth.ConfirmPasswordReset;
using TaskManagementAPI.Application.Features.Commands.Auth.FacebookLoginUser;
using TaskManagementAPI.Application.Features.Commands.Auth.GenerateConfirmEmailToken;
using TaskManagementAPI.Application.Features.Commands.Auth.GoogleLoginUser;
using TaskManagementAPI.Application.Features.Commands.Auth.Login;
using TaskManagementAPI.Application.Features.Commands.Auth.PasswordReset;
using TaskManagementAPI.Application.Features.Commands.Auth.RefreshTokenLoginUser;
using TaskManagementAPI.WebAPI.Configurations;

namespace TaskManagementAPI.WebAPI.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [AllowAnonymous]
        [HttpPost(ApiRoutes.Authentication.GenerateConfirmEmailToken)]
        public async Task<IActionResult> GenerateConfirmEmailToken ([FromBody] GenerateConfirmEmailTokenCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Authentication.ConfirmEmail)]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
        
        [AllowAnonymous]
        [HttpPost(ApiRoutes.Authentication.PasswordReset)]
        public async Task<IActionResult> PasswordReset([FromBody] PasswordResetCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Authentication.ConfirmPasswordReset)]
        public async Task<IActionResult> ConfirmPasswordReset([FromBody] ConfirmPasswordResetCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Authentication.Login)]
        public async Task<IActionResult> Login([FromBody] LoginCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Authentication.GoogleLogin)]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginUserCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Authentication.FacebookLogin)]
        public async Task<IActionResult> FacebookLogin([FromBody] FacebookLoginUserCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        
        [AllowAnonymous]
        [HttpPost(ApiRoutes.Authentication.RefreshTokenLogin)]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] RefreshTokenLoginUserCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
