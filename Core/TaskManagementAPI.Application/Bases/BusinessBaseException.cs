using Microsoft.AspNetCore.Identity;
using TaskManagementAPI.Application.Extensions;

namespace TaskManagementAPI.Application.Bases;

public class BusinessBaseException : Exception
{
    public BusinessBaseException(string message) : base(message) { }

    public BusinessBaseException(string message, IdentityResult identityResult) : base(message)
    {
        ErrorMessages = new() { message };
        ErrorMessages.AddRange(identityResult.ToStringList());
    }
    public BusinessBaseException(string message, Exception innerException) : base(message, innerException) { }
    public List<string>? ErrorMessages { get; set; }
}
