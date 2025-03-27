using AppointmentSystemServer.Application.Features.Appointments._Rules;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace AppointmentSystemServer.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        services.AddScoped(typeof(AppointmentBusinessRules));


        services.AddAutoMapper(assembly);
        services.AddMediatR(configuration => { configuration.RegisterServicesFromAssembly(assembly); });
        return services;
    }
}
