using TaskManagementAPI.Application.DTOs;

namespace TaskManagementAPI.Application.Features.Commands.Project.CreateProject;

public class CreateProjectCommandResponse
{
    public CreateProjectCommandResponse()
    {
        Errors = new List<string>();
    }

    public CreateProjectDTO? CreateProjectDTO { get; set; }
    public IEnumerable<string> Errors { get; set; }
    //public bool Succeeded { get; set; }
    //public string Message { get; set; }
}
