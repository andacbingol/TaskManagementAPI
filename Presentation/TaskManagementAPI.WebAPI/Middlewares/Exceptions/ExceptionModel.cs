using System.Text.Json;

namespace TaskManagementAPI.WebAPI.Middlewares.Exceptions;

public class ExceptionModel
{
    public IEnumerable<string?> Errors { get; set; }
    public int StatusCode { get; set; }
    public override string ToString() => JsonSerializer.Serialize(this);
}
