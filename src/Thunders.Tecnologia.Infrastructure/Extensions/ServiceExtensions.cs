using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Thunders.Tecnologia.Domain.Interfaces;
using Thunders.Tecnologia.Infrastructure.Persistence;
using Thunders.Tecnologia.Infrastructure.Repositories;

namespace Thunders.Tecnologia.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        // Apply pending migrations during application startup
        using var scope = services.BuildServiceProvider().CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            dbContext.Database.Migrate();
        }

        services.AddScoped<IPeopleRepository, PeopleRepository>();

        return services;
    }
}