using AutoMapper;
using MediatR;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs;
using TaskManagementAPI.Application.Features.Rules.Task;

namespace TaskManagementAPI.Application.Features.Queries.Task.GetTaskById;
public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQueryRequest, GetTaskByIdQueryResponse>
{
    private readonly ITaskService _taskService;
    private readonly IUserClaimService _userClaimService;
    private readonly GetTaskByIdRules _getTaskByIdRules;
    private readonly IMapper _mapper;

    public GetTaskByIdQueryHandler(ITaskService taskService, IUserClaimService userClaimService, GetTaskByIdRules getTaskByIdRules, IMapper mapper)
    {
        _taskService = taskService;
        _userClaimService = userClaimService;
        _getTaskByIdRules = getTaskByIdRules;
        _mapper = mapper;
    }

    public async Task<GetTaskByIdQueryResponse> Handle(GetTaskByIdQueryRequest request, CancellationToken cancellationToken)
    {
        Guid claimUserId = _userClaimService.GetClaimUserId();

        await _getTaskByIdRules.CheckAdminOrAccountOwnerAsync(claimUserId, request.UserId);

        await _getTaskByIdRules.CheckUserIsProjectOwnerAsync(request.ProjectId, request.UserId);

        TaskDTO? taskDTO = await _taskService.GetProjectTaskById(request.Id, request.ProjectId);

        return _mapper.Map<GetTaskByIdQueryResponse>(taskDTO);
    }
}
