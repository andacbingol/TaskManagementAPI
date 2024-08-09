using AutoMapper;
using MediatR;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs;

namespace TaskManagementAPI.Application.Features.Queries.Role.GetRoleById;
public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQueryRequest, GetRoleByIdQueryResponse>
{
    private readonly IRoleService _roleService;
    private readonly IMapper _mapper;

    public GetRoleByIdQueryHandler(IRoleService roleService, IMapper mapper)
    {
        _roleService = roleService;
        _mapper = mapper;
    }

    public async Task<GetRoleByIdQueryResponse> Handle(GetRoleByIdQueryRequest request, CancellationToken cancellationToken)
    {
        RoleDTO roleDTO = await _roleService.GetRoleByIdAsync(request.Id);

        return _mapper.Map<GetRoleByIdQueryResponse>(roleDTO);
    }
}
