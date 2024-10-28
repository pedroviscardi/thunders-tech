using Microsoft.Extensions.DependencyInjection;

namespace Thunders.Tecnologia.Application.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            configuration.RegisterServicesFromAssemblies(assemblies.ToArray());
        });

        return services;
    }
}