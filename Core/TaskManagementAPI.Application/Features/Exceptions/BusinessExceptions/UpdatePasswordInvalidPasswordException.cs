using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Exceptions.BusinessExceptions;

public class UpdatePasswordInvalidPasswordException : BusinessBaseException
{
    public UpdatePasswordInvalidPasswordException(string message) : base(message)
    {
    }

    public UpdatePasswordInvalidPasswordException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
