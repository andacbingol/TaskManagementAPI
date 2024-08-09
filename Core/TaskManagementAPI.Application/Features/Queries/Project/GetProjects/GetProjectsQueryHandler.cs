using AutoMapper;
using MediatR;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs;
using TaskManagementAPI.Application.Features.Rules.Project;

namespace TaskManagementAPI.Application.Features.Queries.Project.GetProjects;

public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQueryRequest, List<GetProjectsQueryResponse>>
{
    private readonly IProjectService _projectService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly GetProjectsRules _getProjectsRules;
    private readonly IUserClaimService _userClaimService;

    public GetProjectsQueryHandler(IProjectService projectService, IMapper mapper, IUserService userService, GetProjectsRules getProjectsRules, IUserClaimService userClaimService)
    {
        _projectService = projectService;
        _mapper = mapper;
        _userService = userService;
        _getProjectsRules = getProjectsRules;
        _userClaimService = userClaimService;
    }

    public async Task<List<GetProjectsQueryResponse>> Handle(GetProjectsQueryRequest request, CancellationToken cancellationToken)
    {
        Guid claimUserId = _userClaimService.GetClaimUserId();
        //if (await _userService.FindByIdAsync(request.UserId) is null)
        //    throw new UserNotFoundException();

        await _getProjectsRules.CheckAdminOrAccountOwnerAsync(claimUserId, request.UserId);

        ProjectFilterDTO projectFilter = _mapper.Map<ProjectFilterDTO>(request);

        var datas = await _projectService.GetUserProjectsAsync(projectFilter, request.UserId);

        return datas.Select(p => _mapper.Map<GetProjectsQueryResponse>(p)).ToList();
    }
}
