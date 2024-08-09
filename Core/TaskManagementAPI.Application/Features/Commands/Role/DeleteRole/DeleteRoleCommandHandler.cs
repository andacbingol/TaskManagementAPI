using MediatR;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.Features.Rules.Role;

namespace TaskManagementAPI.Application.Features.Commands.Role.DeleteRole;
public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommandRequest, DeleteRoleCommandResponse>
{
    private readonly IRoleService _roleService;
    private readonly DeleteRoleRules _deleteRoleRules;

    public DeleteRoleCommandHandler(IRoleService roleService, DeleteRoleRules deleteRoleRules)
    {
        _roleService = roleService;
        _deleteRoleRules = deleteRoleRules;
    }

    public async Task<DeleteRoleCommandResponse> Handle(DeleteRoleCommandRequest request, CancellationToken cancellationToken)
    {
        await _deleteRoleRules.CheckRoleIsCriticalAsync(request.Id);

        await _deleteRoleRules.CheckIfRoleIsInUseAsync(request.Id);

        await _roleService.DeleteRoleByIdAsync(request.Id);

        return new() { Succeeded = true };
    }
}
