namespace AppointmentSystemServer.Infrastructure.Caching;

public interface ICacheService
{
    Task<T> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> fetchData, TimeSpan? absoluteExpiration = null);
    Task RemoveAsync(string cacheKey);


    /// <summary>
    /// İstenilen key yapıları silinir
    /// </summary>
    /// <param name="prefix"></param>
    /// <returns></returns>
    Task RemoveByPrefixAsync(List<string> prefix);


    /// <summary>
    /// Tüm keyler silinir
    /// </summary>
    /// <param name="prefix"></param>
    /// <returns></returns>
    Task RemoveAllByPrefixAsync(string prefix);
}