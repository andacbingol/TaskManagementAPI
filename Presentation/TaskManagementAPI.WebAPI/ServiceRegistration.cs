using TaskManagementAPI.WebAPI.Middlewares.Exceptions;

namespace TaskManagementAPI.WebAPI;

public static class ServiceRegistration
{
    public static void AddWebAPIServices(this IServiceCollection services)
    {
        services.AddTransient<ExceptionMiddleware>();
    }
}
