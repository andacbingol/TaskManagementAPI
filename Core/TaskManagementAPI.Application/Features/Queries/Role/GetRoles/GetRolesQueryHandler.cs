using AutoMapper;
using MediatR;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs;

namespace TaskManagementAPI.Application.Features.Queries.Role.GetRoles;
public class GetRolesQueryHandler : IRequestHandler<GetRolesQueryRequest, List<GetRolesQueryResponse>>
{
    private readonly IRoleService _roleService;
    private readonly IMapper _mapper;

    public GetRolesQueryHandler(IRoleService roleService, IMapper mapper)
    {
        _roleService = roleService;
        _mapper = mapper;
    }

    public async Task<List<GetRolesQueryResponse>> Handle(GetRolesQueryRequest request, CancellationToken cancellationToken)
    {
        List<RoleDTO> datas = await _roleService.GetRolesAsync();

        return datas
            .Select(d => _mapper.Map<GetRolesQueryResponse>(d))
            .ToList();
    }
}
