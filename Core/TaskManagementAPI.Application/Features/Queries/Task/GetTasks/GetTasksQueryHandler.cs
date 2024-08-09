using AutoMapper;
using MediatR;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs;
using TaskManagementAPI.Application.Features.Rules.Task;

namespace TaskManagementAPI.Application.Features.Queries.Task.GetTasks;
public class GetTasksQueryHandler : IRequestHandler<GetTasksQueryRequest, List<GetTasksQueryResponse>>
{
    private readonly ITaskService _taskService;
    private readonly IMapper _mapper;
    private readonly IUserClaimService _userClaimService;
    private readonly GetTasksRules _getTasksRules;

    public GetTasksQueryHandler(ITaskService taskService, IMapper mapper, IUserClaimService userClaimService, GetTasksRules getTasksRules)
    {
        _taskService = taskService;
        _mapper = mapper;
        _userClaimService = userClaimService;
        _getTasksRules = getTasksRules;
    }

    public async Task<List<GetTasksQueryResponse>> Handle(GetTasksQueryRequest request, CancellationToken cancellationToken)
    {
        Guid claimUserId = _userClaimService.GetClaimUserId();

        await _getTasksRules.CheckAdminOrAccountOwnerAsync(claimUserId, request.UserId);

        await _getTasksRules.CheckUserIsProjectOwnerAsync(request.ProjectId, request.UserId);

        TaskFilterDTO taskFilterDTO = _mapper.Map<TaskFilterDTO>(request);

        var datas = await _taskService.GetProjectTasksAsync(taskFilterDTO, request.ProjectId);

        return datas.Select(t => _mapper.Map<GetTasksQueryResponse>(t)).ToList();
    }
}
