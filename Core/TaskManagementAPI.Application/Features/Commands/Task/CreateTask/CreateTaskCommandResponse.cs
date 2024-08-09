using TaskManagementAPI.Application.DTOs;

namespace TaskManagementAPI.Application.Features.Commands.Task.CreateTask;
public class CreateTaskCommandResponse
{
    public CreateTaskCommandResponse()
    {
        Errors = new List<string>();
    }
    public CreateTaskDTO? CreateTaskDTO { get; set; }
    public IEnumerable<string> Errors { get; set; }
}
