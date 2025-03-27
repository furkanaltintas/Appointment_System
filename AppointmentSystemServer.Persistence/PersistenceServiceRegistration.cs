using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Persistence.Context;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Reflection;

namespace AppointmentSystemServer.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        services.AddDbContext<AppDbContext>(options => { options.UseSqlServer(configuration.GetConnectionString("Local")).EnableSensitiveDataLogging(); });

        services.AddIdentity<AppUser, AppRole>(action =>
        {
            action.Password.RequiredLength = 5;
            action.Password.RequireUppercase = false;
            action.Password.RequireLowercase = false;
            action.Password.RequireNonAlphanumeric = false;
            action.Password.RequireDigit = false;
        }).AddEntityFrameworkStores<AppDbContext>();

        services.AddScoped<IUnitOfWork>(imp => imp.GetRequiredService<AppDbContext>());

        services.Scan(action =>
        {
            action.FromAssemblies(assembly)
            .AddClasses(publicOnly: false)
            .UsingRegistrationStrategy(registrationStrategy: RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime();
        });

        return services;
    }
}