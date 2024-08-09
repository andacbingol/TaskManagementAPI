using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Application.RequestParameters;
using TaskManagementAPI.Domain.Enums;

namespace TaskManagementAPI.Application.Features.Queries.Task.GetTasks;
public class GetTasksQueryRequest : IRequest<List<GetTasksQueryResponse>>
{
    [FromRoute(Name = "userId")]
    public Guid UserId { get; set; }
    [FromRoute(Name = "projectId")]
    public Guid ProjectId { get; set; }
    [FromQuery]
    public string? Title { get; set; }
    [FromQuery]
    public Priority? Priority { get; set; }
    [FromQuery]
    public Status? Status { get; set; }
    [FromQuery]
    public DateTime? DueDate { get; set; }
    [FromQuery]
    public DateTime? CreatedDate { get; set; }
    [FromQuery]
    public DateTime? UpdatedDate { get; set; }
    [FromQuery]
    public Pagination? Pagination { get; set; }
    [FromQuery]
    public string? SortBy { get; set; }
    [FromQuery]
    public string? SortOrder { get; set; }
}
