namespace TaskManagementAPI.Domain.Constants;

public static class Roles
{
    public const string Admin = "Admin";
    public const string AppUser = "AppUser";
    public const string AdminOrUser = "Admin" + "," + AppUser;
}
