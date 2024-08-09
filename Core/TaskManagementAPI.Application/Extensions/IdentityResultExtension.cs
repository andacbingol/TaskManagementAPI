using Microsoft.AspNetCore.Identity;

namespace TaskManagementAPI.Application.Extensions;

public static class IdentityResultExtension
{
    public static List<string> ToStringList(this IdentityResult result)
    {
        return result.Errors.Select(e => $"{e.Code} - {e.Description}").ToList();
    }
}
