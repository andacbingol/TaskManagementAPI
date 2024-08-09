using AutoMapper;
using MediatR;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs;
using TaskManagementAPI.Application.Features.Rules.Role;

namespace TaskManagementAPI.Application.Features.Commands.Role.UpdateRole;
public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommandRequest, UpdateRoleCommandResponse>
{
    private readonly IRoleService _roleService;
    private readonly IMapper _mapper;
    private readonly UpdateRoleRules _updateRoleRules;

    public UpdateRoleCommandHandler(IRoleService roleService, IMapper mapper, UpdateRoleRules updateRoleRules)
    {
        _roleService = roleService;
        _mapper = mapper;
        _updateRoleRules = updateRoleRules;
    }
    public async Task<UpdateRoleCommandResponse> Handle(UpdateRoleCommandRequest request, CancellationToken cancellationToken)
    {
        await _updateRoleRules.RoleNameAlreadyExist(request.UpdateRoleCommandRequestBody.Name);

        UpdateRoleDTO updateRoleDTO = _mapper.Map<UpdateRoleDTO>(request);

        bool result = await _roleService.UpdateRoleAsync(updateRoleDTO);

        return new() { Success = result };
    }
}
