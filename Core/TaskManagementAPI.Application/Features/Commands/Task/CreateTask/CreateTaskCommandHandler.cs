using AutoMapper;
using MediatR;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs;
using TaskManagementAPI.Application.Features.Rules.Task;

namespace TaskManagementAPI.Application.Features.Commands.Task.CreateTask;
public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommandRequest, CreateTaskCommandResponse>
{
    private readonly ITaskService _taskService;
    private readonly CreateTaskRules _createTaskRules;
    private readonly IMapper _mapper;
    private readonly IUserClaimService _userClaimService;

    public CreateTaskCommandHandler(ITaskService taskService, IMapper mapper, IUserClaimService userClaimService, CreateTaskRules createTaskRules)
    {
        _taskService = taskService;
        _mapper = mapper;
        _userClaimService = userClaimService;
        _createTaskRules = createTaskRules;
    }

    public async Task<CreateTaskCommandResponse> Handle(CreateTaskCommandRequest request, CancellationToken cancellationToken)
    {
        Guid claimUserId = _userClaimService.GetClaimUserId();


        CreateTaskDTO createTaskDTO = _mapper.Map<CreateTaskDTO>(request);
        createTaskDTO.ProjectId = request.ProjectId;

        await _createTaskRules.CheckAdminOrAccountOwnerAsync(claimUserId, request.UserId);

        await _createTaskRules.CheckUserIsProjectOwnerAsync(request.ProjectId, request.UserId);

        await _createTaskRules.TaskTitleAlreadyExistForThisProject(createTaskDTO.Id, createTaskDTO.Title, createTaskDTO.ProjectId);

        await _createTaskRules.TaskIdAlreadyExistFor(createTaskDTO.Id);

        bool result = await _taskService.CreateTaskAsync(createTaskDTO);

        return new()
        {
            CreateTaskDTO = result ? createTaskDTO : null,
            Errors = result ? new List<string>() : new List<string> { "Failed to create task" }
        };
    }
}
