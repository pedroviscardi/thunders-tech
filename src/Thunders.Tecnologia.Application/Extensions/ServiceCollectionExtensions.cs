using Microsoft.Extensions.DependencyInjection;
using Thunders.Tecnologia.Application.Interfaces;
using Thunders.Tecnologia.Application.Mapping;
using Thunders.Tecnologia.Application.Services;

namespace Thunders.Tecnologia.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddMediatR(configuration =>
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            configuration.RegisterServicesFromAssemblies(assemblies.ToArray());
        });

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<ITaskService, TaskService>();
        return services;
    }
}