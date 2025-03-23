using AppointmentSystemServer.Infrastructure.Caching;
using AppointmentSystemServer.Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentSystemServer.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ICacheService, CacheService>();
        return services;
    }
}
