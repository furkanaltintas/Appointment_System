namespace AppointmentSystemServer.Infrastructure.Caching;

public interface ICacheService
{
    Task<T> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> fetchData, TimeSpan? absoluteExpiration = null);
    Task RemoveAsync(string cacheKey);
}