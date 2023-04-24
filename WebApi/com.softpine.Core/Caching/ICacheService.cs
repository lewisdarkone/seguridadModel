namespace com.softpine.muvany.core.Interfaces;

/// <summary>
/// 
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    T? Get<T>(string key);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<T?> GetAsync<T>(string key, CancellationToken token = default);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    void Refresh(string key);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task RefreshAsync(string key, CancellationToken token = default);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    void Remove(string key);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task RemoveAsync(string key, CancellationToken token = default);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="slidingExpiration"></param>
    void Set<T>(string key, T value, TimeSpan? slidingExpiration = null);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="slidingExpiration"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SetAsync<T>(string key, T value, TimeSpan? slidingExpiration = null, CancellationToken cancellationToken = default);
}
