using AutoMapper;
using MediatR;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs.Task;
using TaskManagementAPI.Application.Features.Rules.Task;

namespace TaskManagementAPI.Application.Features.Commands.Task.UpdateTask;
public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommandRequest, UpdateTaskCommandResponse>
{
    private readonly ITaskService _taskService;
    private readonly IMapper _mapper;
    private readonly IUserClaimService _userClaimService;
    private readonly UpdateTaskRules _updateTaskRules;

    public UpdateTaskCommandHandler(ITaskService taskService, IMapper mapper, IUserClaimService userClaimService, UpdateTaskRules updateTaskRules)
    {
        _taskService = taskService;
        _mapper = mapper;
        _userClaimService = userClaimService;
        _updateTaskRules = updateTaskRules;
    }

    public async Task<UpdateTaskCommandResponse> Handle(UpdateTaskCommandRequest request, CancellationToken cancellationToken)
    {
        Guid claimUserId = _userClaimService.GetClaimUserId();

        await _updateTaskRules.CheckAdminOrAccountOwnerAsync(claimUserId, request.UserId);

        await _updateTaskRules.CheckUserIsProjectOwnerAsync(request.ProjectId, request.UserId);

        await _updateTaskRules.CheckTaskTitleAlreadyExistForProject(request.Id, request.UpdateTaskCommandRequestBody.Title, request.ProjectId);

        bool result = await _taskService.UpdateTaskAsync(_mapper.Map<UpdateTaskDTO>(request));

        return new()
        {
            Success = result,
        };
    }
}
