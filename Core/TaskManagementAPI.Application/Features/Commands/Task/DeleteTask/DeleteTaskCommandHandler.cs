using AutoMapper;
using MediatR;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.Features.Rules.Task;

namespace TaskManagementAPI.Application.Features.Commands.Task.DeleteTask;
public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommandRequest, DeleteTaskCommandResponse>
{
    private readonly ITaskService _taskService;
    private readonly IMapper _mapper;
    private readonly IUserClaimService _userClaimService;
    private readonly DeleteTaskRules _deleteTaskRules;
    public DeleteTaskCommandHandler(ITaskService taskService, IMapper mapper, IUserClaimService userClaimService, DeleteTaskRules deleteTaskRules)
    {
        _taskService = taskService;
        _mapper = mapper;
        _userClaimService = userClaimService;
        _deleteTaskRules = deleteTaskRules;
    }
    public async Task<DeleteTaskCommandResponse> Handle(DeleteTaskCommandRequest request, CancellationToken cancellationToken)
    {
        Guid claimUserId = _userClaimService.GetClaimUserId();

        await _deleteTaskRules.CheckAdminOrAccountOwnerAsync(claimUserId, request.UserId);

        await _deleteTaskRules.CheckUserIsProjectOwnerAsync(request.ProjectId, request.UserId);

        var result = await _taskService.RemoveTaskByIdAsync(request.Id);

        return new() { Succeeded = result, };
    }
}
