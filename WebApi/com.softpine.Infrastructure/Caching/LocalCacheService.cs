using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using com.softpine.muvany.core.Interfaces;

namespace com.softpine.muvany.infrastructure.Caching;

/// <summary>
/// 
/// </summary>
public class LocalCacheService : ICacheService
{
    private readonly ILogger<LocalCacheService> _logger;
    private readonly IMemoryCache _cache;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cache"></param>
    /// <param name="logger"></param>
    public LocalCacheService(IMemoryCache cache, ILogger<LocalCacheService> logger) =>
        (_cache, _logger) = (cache, logger);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public T? Get<T>(string key) =>
        _cache.Get<T>(key);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<T?> GetAsync<T>(string key, CancellationToken token = default) =>
        Task.FromResult(Get<T>(key));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    public void Refresh(string key) =>
        _cache.TryGetValue(key, out _);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task RefreshAsync(string key, CancellationToken token = default)
    {
        Refresh(key);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    public void Remove(string key) =>
        _cache.Remove(key);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task RemoveAsync(string key, CancellationToken token = default)
    {
        Remove(key);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="slidingExpiration"></param>
    public void Set<T>(string key, T value, TimeSpan? slidingExpiration = null)
    {
        if (slidingExpiration is null)
        {
            
            slidingExpiration = TimeSpan.FromMinutes(10); // Default expiration time of 10 minutes.
        }

        _cache.Set(key, value, new MemoryCacheEntryOptions { SlidingExpiration = slidingExpiration });
        _logger.LogDebug($"Added to Cache : {key}", key);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="slidingExpiration"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task SetAsync<T>(string key, T value, TimeSpan? slidingExpiration = null, CancellationToken token = default)
    {
        Set(key, value, slidingExpiration);
        return Task.CompletedTask;
    }
}
