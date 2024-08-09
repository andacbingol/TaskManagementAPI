using MediatR;
using TaskManagementAPI.Application.Abstractions.Services;

namespace TaskManagementAPI.Application.Features.Commands.User.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommandRequest, DeleteUserCommandResponse>
{
    private readonly IUserService _userService;

    public DeleteUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<DeleteUserCommandResponse> Handle(DeleteUserCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.DeleteUserAsync(request.Id);
        return new()
        {
            Succeeded = result,
        };
    }
}
