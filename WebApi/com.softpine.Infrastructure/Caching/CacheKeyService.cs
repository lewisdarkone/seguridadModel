
using com.softpine.muvany.core.Services;

namespace com.softpine.muvany.infrastructure.Caching;

/// <summary>
/// 
/// </summary>
public class CacheKeyService : ICacheKeyService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public string GetCacheKey(string name, object id)
    {
        string tenantId = "GLOBAL";
        return $"{tenantId}-{name}-{id}";
    }
}
