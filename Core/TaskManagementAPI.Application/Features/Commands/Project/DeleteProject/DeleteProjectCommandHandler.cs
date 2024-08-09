using MediatR;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.Features.Rules.Project;

namespace TaskManagementAPI.Application.Features.Commands.Project.DeleteProject
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommandRequest, DeleteProjectCommandResponse>
    {
        private readonly IProjectService _projectService;
        private readonly DeleteProjectRules _deleteProjectRules;
        private readonly IUserClaimService _userClaimService;
        public DeleteProjectCommandHandler(IProjectService projectService, DeleteProjectRules deleteProjectRules, IUserClaimService userClaimService)
        {
            _projectService = projectService;
            _deleteProjectRules = deleteProjectRules;
            _userClaimService = userClaimService;
        }

        public async Task<DeleteProjectCommandResponse> Handle(DeleteProjectCommandRequest request, CancellationToken cancellationToken)
        {
            Guid claimUserId = _userClaimService.GetClaimUserId();

            await _deleteProjectRules.CheckAdminOrAccountOwnerAsync(claimUserId, request.UserId);

            var result = await _projectService.RemoveProjectByIdAsync(request.Id);
            
            return new() { Succeeded = result };
        }
    }
}
