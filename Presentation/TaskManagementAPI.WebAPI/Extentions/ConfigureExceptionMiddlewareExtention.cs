using TaskManagementAPI.WebAPI.Middlewares.Exceptions;

namespace TaskManagementAPI.WebAPI.Extentions;

public static class ConfigureExceptionMiddlewareExtention
{
    public static void ConfigureExceptionHandlingMiddleware(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<ExceptionMiddleware>();
    }
}
