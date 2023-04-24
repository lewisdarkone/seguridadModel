using com.softpine.muvany.core.Interfaces;

namespace com.softpine.muvany.core.Extensions;

/// <summary>
/// 
/// </summary>
public static class CacheServiceExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cache"></param>
    /// <param name="key"></param>
    /// <param name="getItemCallback"></param>
    /// <param name="slidingExpiration"></param>
    /// <returns></returns>
    public static T? GetOrSet<T>(this ICacheService cache, string key, Func<T?> getItemCallback, TimeSpan? slidingExpiration = null)
    {
        T? value = cache.Get<T>(key);

        if (value is not null)
        {
            return value;
        }

        value = getItemCallback();

        if (value is not null)
        {
            cache.Set(key, value, slidingExpiration);
        }

        return value;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cache"></param>
    /// <param name="key"></param>
    /// <param name="getItemCallback"></param>
    /// <param name="slidingExpiration"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<T?> GetOrSetAsync<T>(this ICacheService cache, string key, Func<Task<T>> getItemCallback, TimeSpan? slidingExpiration = null, CancellationToken cancellationToken = default)
    {
        T? value = await cache.GetAsync<T>(key, cancellationToken);

        if (value is not null)
        {
            return value;
        }

        value = await getItemCallback();

        if (value is not null)
        {
            await cache.SetAsync(key, value, slidingExpiration, cancellationToken);
        }

        return value;
    }
}
