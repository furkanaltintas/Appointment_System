using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace AppointmentSystemServer.Infrastructure.Caching;

public class CacheService(IDistributedCache distributedCache) : ICacheService
{
    public async Task<T> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> fetchData, TimeSpan? absoluteExpiration = null)
    {
        // Cache'ten veriyi al
        string? cachedData = await distributedCache.GetStringAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedData))
        {
            // Cache'ten veri varsa deserialize et
            return JsonSerializer.Deserialize<T>(cachedData);
        }

        // Cache'te veri yoksa, fetchData fonksiyonu ile veriyi al
        var data = await fetchData();

        // Cache'e veriyi serialize edip ekle
        var serializedData = JsonSerializer.Serialize(data);

        // Cache süresini belirleyin
        DistributedCacheEntryOptions cacheOptions = new()
        {
            AbsoluteExpirationRelativeToNow = absoluteExpiration ?? TimeSpan.FromMinutes(20)  // Varsayılan 20 dakika
        };

        await distributedCache.SetStringAsync(cacheKey, serializedData, cacheOptions);
        return data;
    }

    public async Task RemoveAsync(string cacheKey) => await distributedCache.RemoveAsync(cacheKey);
}