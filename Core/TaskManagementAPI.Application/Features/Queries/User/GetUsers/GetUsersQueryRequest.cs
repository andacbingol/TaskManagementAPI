using MediatR;
using TaskManagementAPI.Application.RequestParameters;

namespace TaskManagementAPI.Application.Features.Queries.User.GetUsers;

public class GetUsersQueryRequest : IRequest<List<GetUsersQueryResponse>>
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public List<string>? Roles { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public Pagination? Pagination { get; set; }
    public string? SortBy { get; set; }
    public string? SortOrder { get; set; }
}
