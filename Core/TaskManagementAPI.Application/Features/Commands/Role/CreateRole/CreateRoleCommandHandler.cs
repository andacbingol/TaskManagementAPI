using AutoMapper;
using MediatR;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs.Role;
using TaskManagementAPI.Application.Features.Rules.Role;

namespace TaskManagementAPI.Application.Features.Commands.Role.CreateRole;
public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommandRequest, CreateRoleCommandResponse>
{
    private readonly IRoleService _roleService;
    private readonly IMapper _mapper;
    private readonly CreateRoleRules _createRoleRules;

    public CreateRoleCommandHandler(IRoleService roleService, IMapper mapper, CreateRoleRules createRoleRules)
    {
        _roleService = roleService;
        _mapper = mapper;
        _createRoleRules = createRoleRules;
    }

    public async Task<CreateRoleCommandResponse> Handle(CreateRoleCommandRequest request, CancellationToken cancellationToken)
    {
        CreateRoleDTO createRoleDTO = _mapper.Map<CreateRoleDTO>(request);
        
        await _createRoleRules.RoleNameAlreadyExist(createRoleDTO.Name);

        await _createRoleRules.RoleIdAlreadyExist(createRoleDTO.Id);

        await _roleService.CreateRoleAsync(createRoleDTO);

        return new() { Id = createRoleDTO.Id, Name = createRoleDTO.Name };
    }
}
