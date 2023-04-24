namespace com.softpine.muvany.infrastructure.Caching;

/// <summary>
/// 
/// </summary>
public class CacheSettings
{
    /// <summary>
    /// 
    /// </summary>
    public bool UseDistributedCache { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool PreferRedis { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? RedisURL { get; set; }
}
