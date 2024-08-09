using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Application.RequestParameters;

namespace TaskManagementAPI.Application.Features.Queries.Project.GetProjects;

public class GetProjectsQueryRequest : IRequest<List<GetProjectsQueryResponse>>
{
    [FromRoute(Name ="userId")]
    public Guid UserId { get; set; }

    [FromQuery]
    public string? Name { get; set; }
    [FromQuery]
    public DateTime? StartDate { get; set; }
    [FromQuery]
    public DateTime? EndDate { get; set; }
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
