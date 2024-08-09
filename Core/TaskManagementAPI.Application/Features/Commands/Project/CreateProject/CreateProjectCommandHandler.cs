using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs;
using TaskManagementAPI.Application.Features.Exceptions.AppInternalExceptions;
using TaskManagementAPI.Application.Features.Rules.Project;

namespace TaskManagementAPI.Application.Features.Commands.Project.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommandRequest, CreateProjectCommandResponse>
{
    private readonly IProjectService _projectService;
    private readonly IMapper _mapper;
    private readonly CreateProjectRules _createProjectRules;
    private readonly IUserClaimService _userClaimService;

    public CreateProjectCommandHandler(IProjectService projectService, IMapper mapper, CreateProjectRules createProjectRules, IUserClaimService userClaimService)
    {
        _projectService = projectService;
        _mapper = mapper;
        _createProjectRules = createProjectRules;
        _userClaimService = userClaimService;
    }

    public async Task<CreateProjectCommandResponse> Handle(CreateProjectCommandRequest request, CancellationToken cancellationToken)
    {
        Guid claimUserId = _userClaimService.GetClaimUserId();

        CreateProjectDTO createProjectDTO = _mapper.Map<CreateProjectDTO>(request);

        await _createProjectRules.CheckAdminOrAccountOwnerAsync(claimUserId, createProjectDTO.UserId);

        await _createProjectRules.ProjectNameAlreadyExistForAccount(createProjectDTO.Id, createProjectDTO.Name, createProjectDTO.UserId);

        await _createProjectRules.ProjectIdAlreadyExist(createProjectDTO.Id);


        bool result = await _projectService.CreateProjectAsync(createProjectDTO);

        return new()
        {
            CreateProjectDTO = result ? createProjectDTO : null,
            Errors = result ? new List<string>() : new List<string> { "Failed to create project" }
        }; 
    }
}
