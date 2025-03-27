using AppointmentSystemServer.Infrastructure.Caching;
using AppointmentSystemServer.Infrastructure.Services;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace AppointmentSystemServer.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddMemoryCache();


        string? redisConnection = configuration.GetConnectionString("Redis") ?? "localhost:6379";

        // Redis bağlantısı kurma
        IConnectionMultiplexer? redisConnectionMultiplexer = null;
        try
        {
            redisConnectionMultiplexer = ConnectionMultiplexer.Connect(redisConnection);
            Console.WriteLine("✅ Redis bağlantısı başarılı.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Redis bağlantı hatası: {ex.Message}. MemoryCache kullanılacak.");
        }

        // Redis bağlantısı varsa, Redis tabanlı cache servisi kullanacağız. Aksi takdirde, InMemoryCache kullanılacak.
        if (redisConnectionMultiplexer != null)
        {
            services.AddSingleton<IConnectionMultiplexer>(redisConnectionMultiplexer);

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnection;
            });
        }

        // CacheService'in gerekli bağımlılıklarını ekle
        services.AddSingleton<ICacheService>((serviceProvider) =>
        {
            var distributedCache = serviceProvider.GetRequiredService<IDistributedCache>();
            var memoryCache = serviceProvider.GetRequiredService<IMemoryCache>();
            return new CacheService(distributedCache, memoryCache, redisConnection);
        });

        return services;
    }
}