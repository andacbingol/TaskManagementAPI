using AutoMapper;
using MediatR;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs;
using TaskManagementAPI.Application.Features.Rules.Project;

namespace TaskManagementAPI.Application.Features.Queries.Project.GetProjectById;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQueryRequest, GetProjectByIdQueryResponse>
{
    private readonly IProjectService _projectService;
    private readonly IUserService _userService;
    private readonly IUserClaimService _userClaimService;
    private readonly IMapper _mapper;
    private readonly GetProjectByIdRules _getProjectByIdRules;

    public GetProjectByIdQueryHandler(IProjectService projectService, IMapper mapper, IUserService userService, GetProjectByIdRules getProjectByIdRules)
    {
        _projectService = projectService;
        _mapper = mapper;
        _userService = userService;
        _getProjectByIdRules = getProjectByIdRules;
    }

    public async Task<GetProjectByIdQueryResponse> Handle(GetProjectByIdQueryRequest request, CancellationToken cancellationToken)
    {
        Guid claimUserId = _userClaimService.GetClaimUserId();

        //if (await _userService.FindByIdAsync(request.UserId) is null)
        //    throw new UserNotFoundException();

        await _getProjectByIdRules.CheckAdminOrAccountOwnerAsync(claimUserId, request.UserId);

        ProjectDTO? projectDTO = await _projectService.GetUserProjectByIdAsync(request.Id, request.UserId);

        return _mapper.Map<GetProjectByIdQueryResponse>(projectDTO);
    }
}
