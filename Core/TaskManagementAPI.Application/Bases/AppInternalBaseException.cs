using Microsoft.AspNetCore.Identity;
using TaskManagementAPI.Application.Extensions;

namespace TaskManagementAPI.Application.Bases;

public class AppInternalBaseException : Exception
{
    public AppInternalBaseException(string message) : base(message) { }

    public AppInternalBaseException(string message, IdentityResult identityResult) : base(message)
    {
        ErrorMessages = new() { message };
        ErrorMessages.AddRange(identityResult.ToStringList());
    }
    public AppInternalBaseException(string message, Exception innerException) : base(message, innerException) { }
    public List<string>? ErrorMessages { get; set; }
}
