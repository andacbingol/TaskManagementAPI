using AutoMapper;
using MediatR;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs;
using TaskManagementAPI.Application.Features.Rules.Project;

namespace TaskManagementAPI.Application.Features.Commands.Project.UpdateProject;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommandRequest, UpdateProjectCommandResponse>
{
    private readonly IProjectService _projectService;
    private readonly IMapper _mapper;
    private readonly IUserClaimService _userClaimService;
    private readonly UpdateProjectRules _updateProjectRules;

    public UpdateProjectCommandHandler(IProjectService projectService, IMapper mapper, UpdateProjectRules updateProjectRules, IUserClaimService userClaimService)
    {
        _projectService = projectService;
        _mapper = mapper;
        _updateProjectRules = updateProjectRules;
        _userClaimService = userClaimService;
    }

    public async Task<UpdateProjectCommandResponse> Handle(UpdateProjectCommandRequest request, CancellationToken cancellationToken)
    {
        Guid claimUserId = _userClaimService.GetClaimUserId();

        await _updateProjectRules.CheckAdminOrAccountOwnerAsync(claimUserId,request.UserId);

        await _updateProjectRules.ProjectNameAlreadyExistForAccount(request.Id, request.UpdateProjectCommandRequestBody.Name, request.UserId);

        bool result = await _projectService.UpdateProjectAsync(_mapper.Map<UpdateProjectDTO>(request));
        return new()
        {
            Success = result
        };
    }
}
