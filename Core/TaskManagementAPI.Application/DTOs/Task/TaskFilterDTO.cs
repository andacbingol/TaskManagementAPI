using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Application.RequestParameters;
using TaskManagementAPI.Domain.Enums;

namespace TaskManagementAPI.Application.DTOs;
public class TaskFilterDTO
{
    public string? Title { get; set; }
    public Priority? Priority { get; set; }
    public Status? Status { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Pagination? Pagination { get; set; }
    public string? SortBy { get; set; }
    public string? SortOrder { get; set; }
}
