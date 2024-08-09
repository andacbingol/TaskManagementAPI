using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TaskManagementAPI.Application.Bases;
using TaskManagementAPI.Application.Behaviours;

namespace TaskManagementAPI.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddHttpClient();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehaviour<,>));
        services.AddRulesFromAssemblyContaining(Assembly.GetExecutingAssembly());
    }
    private static IServiceCollection AddRulesFromAssemblyContaining(this IServiceCollection services, Assembly assembly)
    {
        Type type = typeof(BaseRules);
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && !t.IsAbstract).ToList();
        foreach (var item in types)
            services.AddTransient(item);
        return services;
    }
}