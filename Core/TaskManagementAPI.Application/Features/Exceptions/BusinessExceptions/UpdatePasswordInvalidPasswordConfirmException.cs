using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Exceptions.BusinessExceptions;

public class UpdatePasswordInvalidPasswordConfirmException : BusinessBaseException
{
    public UpdatePasswordInvalidPasswordConfirmException(string message) : base(message)
    {
    }
}
