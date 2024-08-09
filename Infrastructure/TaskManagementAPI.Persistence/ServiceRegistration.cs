using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.Repositories;
using TaskManagementAPI.Domain.Entities;
using TaskManagementAPI.Domain.Entities.Identity;
using TaskManagementAPI.Persistence.Context;
using TaskManagementAPI.Persistence.Repositories;
using TaskManagementAPI.Persistence.Services;

namespace TaskManagementAPI.Persistence;
public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TaskManagementAPIDbContext>(
            options => options.UseNpgsql(
                configuration.GetConnectionString("PostgreSQL"),
                    b => b.MigrationsAssembly(typeof(TaskManagementAPIDbContext).Assembly.FullName)));

        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;

            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;
        })
            .AddEntityFrameworkStores<TaskManagementAPIDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IDataGenerator, DataGenerator>();

        services.Configure<FacebookLoginSettings>(configuration.GetSection("ExternalLoginSettings").GetSection("Facebook"));
        services.Configure<GoogleLoginSettings>(configuration.GetSection("ExternalLoginSettings").GetSection("Google"));

        services.AddRepositories();
        services.AddServices();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        
        services.AddScoped<IProjectReadRepository, ProjectReadRepository>();
        services.AddScoped<IProjectWriteRepository, ProjectWriteRepository>();

        services.AddScoped<ITaskReadRepository, TaskReadRepository>();
        services.AddScoped<ITaskWriteRepository, TaskWriteRepository>();
    }
    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();
    }
}
