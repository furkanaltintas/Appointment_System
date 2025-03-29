using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Polly;
using Polly.Retry;
using StackExchange.Redis;
using System.Diagnostics;
using System.Text.Json;

namespace AppointmentSystemServer.Infrastructure.Caching;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;
    private readonly IMemoryCache _memoryCache;
    private readonly List<string> _cacheKeys = new();

    private readonly AsyncRetryPolicy _retryPolicy;
    private readonly IDatabase _db;
    private readonly ConnectionMultiplexer _redis;


    public CacheService(IDistributedCache distributedCache, IMemoryCache memoryCache, string connectionString)
    {
        _distributedCache = distributedCache;
        _memoryCache = memoryCache;

        if (!string.IsNullOrEmpty(connectionString))
        {
            try
            {
                _redis = ConnectionMultiplexer.Connect(connectionString);
                _db = _redis.GetDatabase();

                // Retry Policy (Hata durumunda 3 kez yeniden deneme)
                _retryPolicy = Policy
                    .Handle<RedisConnectionException>()
                    .Or<TimeoutException>()
                    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromMilliseconds(500)); // 500ms bekleme süresi
            }
            catch (Exception ex)
            {
                _redis = null;
                _db = null;
            }
        }
        else
        {
            _redis = null;
            _db = null;
        }
    }

    public async Task<T> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> fetchData, TimeSpan? absoluteExpiration = null)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();  // Zaman ölçümünü başlat

        try
        {
            // Eğer Redis yoksa, sadece MemoryCache kullan
            if (_distributedCache == null)
            {
                return await GetFromMemoryCache(cacheKey, fetchData, absoluteExpiration);
            }

            // 🔹 MemoryCache'te varsa önce onu döndür
            if (_memoryCache.TryGetValue(cacheKey, out T cachedValue))
            {
                stopwatch.Stop();
                Console.WriteLine($"Cache hit for key {cacheKey} in MemoryCache. Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
                return cachedValue;
            }

            // 🔹 Redis üzerinden veriyi al
            string? cachedData = await _distributedCache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                try
                {
                    var deserializedData = JsonSerializer.Deserialize<T>(cachedData);
                    stopwatch.Stop();
                    Console.WriteLine($"Cache hit for key {cacheKey} in Redis. Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
                    return deserializedData;
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"❌ Deserialize hatası: {ex.Message}. Cache temizlenecek.");
                    await _distributedCache.RemoveAsync(cacheKey);
                }
            }

            // 🔹 Cache'te veri yoksa, fetchData fonksiyonu ile veriyi al
            T data = await fetchData();

            // 🔹 Cache'e veriyi serialize edip ekle
            string serializedData = JsonSerializer.Serialize(data);

            // 🔹 Cache süresini belirle
            DistributedCacheEntryOptions cacheOptions = new()
            {
                AbsoluteExpirationRelativeToNow = absoluteExpiration ?? TimeSpan.FromMinutes(20)
            };

            await _distributedCache.SetStringAsync(cacheKey, serializedData, cacheOptions);

            stopwatch.Stop();
            Console.WriteLine($"Cache miss for key {cacheKey}. Data fetched and stored in Redis and MemoryCache. Elapsed time: {stopwatch.ElapsedMilliseconds} ms");

            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Redis operation failed for key {cacheKey}: {ex.Message}. Falling back to MemoryCache. Elapsed time: {stopwatch.ElapsedMilliseconds} ms");

            // 🔹 MemoryCache kontrol et
            return await GetFromMemoryCache(cacheKey, fetchData, absoluteExpiration);
        }
    }

    // MemoryCache üzerinden veri al
    private async Task<T> GetFromMemoryCache<T>(string cacheKey, Func<Task<T>> fetchData, TimeSpan? absoluteExpiration = null)
    {
        if (_memoryCache.TryGetValue(cacheKey, out T cachedValue))
        {
            return cachedValue;
        }

        // 🔹 Veriyi çek ve MemoryCache'e ekle
        T data = await fetchData();
        _cacheKeys.Add(cacheKey);
        _memoryCache.Set(cacheKey, data, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = absoluteExpiration ?? TimeSpan.FromMinutes(20)
        });

        return data;
    }

    public async Task RemoveAsync(string cacheKey)
    {
        try
        {
            // Redis'ten silme işlemi
            if (_distributedCache != null)
            {
                await _distributedCache.RemoveAsync(cacheKey);
                Console.WriteLine($"Cache entry removed from Redis for key {cacheKey}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Redis silme hatası: {ex.Message}. MemoryCache üzerinden silme işlemi yapılacak.");
        }

        // MemoryCache'ten sil
        _memoryCache.Remove(cacheKey);
        Console.WriteLine($"Cache entry removed from MemoryCache for key {cacheKey}");
    }

    public async Task RemoveByPrefixAsync(List<string> prefix)
    {
        try
        {
            if (_distributedCache != null)
            {
                var server = _redis.GetServer(_redis.GetEndPoints().First());

                foreach (var pre in prefix)
                {
                    var keys = server.Keys(pattern: $"AppointmentSystemCache:{pre}").ToArray(); // {AppointmentSystemCache:GetAllDepartmentQueryHandler}

                    if (keys.Length > 0)
                        await _db.KeyDeleteAsync(keys);
                }
            }
            else
            {
                foreach (var key in _cacheKeys.ToArray())
                {
                    foreach (var pre in prefix)
                    {
                        _cacheKeys.Remove(key);
                        _memoryCache.Remove(key);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Redis silme hatası: {ex.Message}. MemoryCache üzerinden silme işlemi yapılacak.");
        }

    }

    public async Task RemoveAllByPrefixAsync(string prefix)
    {
        try
        {
            if (_distributedCache != null)
            {
                var server = _redis.GetServer(_redis.GetEndPoints().First());

                var keys = server.Keys(pattern: $"AppointmentSystemCache:{prefix}*").ToArray(); // {AppointmentSystemCache:GetAllDepartmentQueryHandler} ile başlayan tüm keyleri alacak

                if (keys.Length > 0)
                    await _db.KeyDeleteAsync(keys);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Redis silme hatası: {ex.Message}. MemoryCache üzerinden silme işlemi yapılacak.");
        }

        foreach (var key in _cacheKeys.ToArray())
        {
            foreach (var pre in prefix)
            {
                _cacheKeys.Remove(key);
                _memoryCache.Remove(key);
            }
        }
    }
}