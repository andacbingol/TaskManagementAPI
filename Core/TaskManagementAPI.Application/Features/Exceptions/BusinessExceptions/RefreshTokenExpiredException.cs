using TaskManagementAPI.Application.Bases;

namespace TaskManagementAPI.Application.Features.Exceptions.BusinessExceptions;

public class RefreshTokenExpiredException : BusinessBaseException
{
    public RefreshTokenExpiredException() : base("RefreshToken login failed! RefreshToken expired!")
    {
    }

    public RefreshTokenExpiredException(string message) : base(message)
    {
    }
}
