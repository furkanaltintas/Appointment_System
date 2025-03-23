using AppointmentSystemServer.Infrastructure.Configurations;

namespace AppointmentSystemServer.WebApi;

public static class PresentationServiceRegistration
{
    public const string AllowSpecificOrigins = "AllowSpecificOrigins";

    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(AllowSpecificOrigins, policy =>
            {
                policy.WithOrigins("http://localhost:4200") // Güvenilir domainleri ekle           
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "localhost:6379";
            options.InstanceName = "AppointmentSystemCache:";
        });

        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        return services;
    }
}