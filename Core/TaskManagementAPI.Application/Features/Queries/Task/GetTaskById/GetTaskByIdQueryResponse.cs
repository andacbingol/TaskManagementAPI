using TaskManagementAPI.Domain.Enums;

namespace TaskManagementAPI.Application.Features.Queries.Task.GetTaskById;
public class GetTaskByIdQueryResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; }
    public DateTime DueDate { get; set; }
}
