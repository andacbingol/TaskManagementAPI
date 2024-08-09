using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Exceptions.BusinessExceptions;

public class ResourceConflictException : BusinessBaseException
{
    public ResourceConflictException(string message) : base(message) { }

    public ResourceConflictException(string message, Exception innerException) : base(message, innerException) { }
}
