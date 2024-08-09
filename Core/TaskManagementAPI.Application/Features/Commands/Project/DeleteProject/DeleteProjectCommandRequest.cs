using MediatR;

namespace TaskManagementAPI.Application.Features.Commands.Project.DeleteProject
{
    public class DeleteProjectCommandRequest : IRequest<DeleteProjectCommandResponse>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
